﻿// This file is part of the GTD.Integra.FocusingRotator project
// 
// Copyright © 2016-2017 Tigra Astronomy., all rights reserved.
// 
// File: ClientConnectionManager.cs  Last modified: 2017-02-27@23:21 by Tim Long

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Ninject;
using NLog;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Threading;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;

namespace TA.DigitalDomeworks.Server
    {
    /// <summary>
    ///     Manages client (driver) connections to the shared device controller. Uses the Reader
    ///     Writer Lock pattern to ensure thread safety.
    /// </summary>
    [ReaderWriterSynchronized]
    public class ClientConnectionManager
        {
        [Reference] readonly ILogger log = LogManager.GetCurrentClassLogger();
        readonly bool performActionsOnOpen;
        [Reference] Maybe<DeviceController> controllerInstance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientConnectionManager" /> class.
        /// </summary>
        /// <param name="factory">
        ///     A factory class that can create and destroy transaction processors (and by implication,
        ///     the entire communications stack).
        /// </param>
        public ClientConnectionManager() : this(performActionsOnOpen: true) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ClientConnectionManager" /> class and allows
        ///     control of whether an On Connected actions will be performed. This is primarily intended
        ///     for use in unit testing so is not visible to clients.
        /// </summary>
        /// <param name="factory">
        ///     A factory class that can construct instances of a trnasaction processor (and by
        ///     implication, the entire communications stack).
        /// </param>
        /// <param name="performActionsOnOpen">
        ///     if set to <c>true</c> [perform actions on open].
        /// </param>
        internal ClientConnectionManager(bool performActionsOnOpen)
            {
            this.performActionsOnOpen = performActionsOnOpen;
            Clients = new List<ClientStatus>();
            controllerInstance = Maybe<DeviceController>.Empty;
            }

        [Reference]
        internal List<ClientStatus> Clients { get; }

        /// <summary>
        ///     Gets the number of connected clients.
        /// </summary>
        /// <value>The connected client count.</value>
        public int RegisteredClientCount => Clients.Count();

        public int OnlineClientCount => Clients.Count(p => p.Online);

        /// <summary>
        ///     Gets the controller instance if it has been created.
        /// </summary>
        /// <value>The controller instance.</value>
        internal Maybe<DeviceController> MaybeControllerInstance => controllerInstance;

        /// <summary>
        ///     Gets the controller instance, ensuring that it is open and ready for use.
        /// </summary>
        /// <param name="clientId">
        ///     The client must provide it's ID which has previously been obtained by calling
        ///     <see cref="RegisterClient" />.
        /// </param>
        /// <returns>IIntegraController.</returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Clients must release previous controller instances before requesting another
        /// </exception>
        [Writer]
        public DeviceController GoOnline(Guid clientId)
            {
            log.Info($"Go online for client {clientId}");
            ClientStatus client = null;
            try
                {
                client = Clients.Single(p => p.Equals(clientId));
                }
            catch (InvalidOperationException e)
                {
                var message = $"Attempt to go online with unregistered client {clientId}";
                log.Error(e, message);
                //ThrowOnUnrecognizedClient(clientId, e, message);
                }

            try
                {
                EnsureControllerInstanceCreatedAndOpen();
                }
            catch (TransactionException trex)
                {
                log.Error(trex, $"NOT CONNECTED due to transaction exception: {trex.Transaction}");
                DestroyControllerInstance();
                return null;
                }

            client.Online = true;
            RaiseClientStatusChanged();
            return controllerInstance.Single();
            }

        internal event EventHandler<EventArgs> ClientStatusChanged;

        protected void RaiseClientStatusChanged()
            {
            ClientStatusChanged?.Invoke(this, EventArgs.Empty);
            }

        [Writer]
        void EnsureControllerInstanceCreatedAndOpen()
            {
            if (!controllerInstance.Any())
                {
                var controller = CompositionRoot.Kernel.Get<DeviceController>();
                controllerInstance = new Maybe<DeviceController>(controller);
                }

            var instance = controllerInstance.Single();
            instance.Open(performActionsOnOpen);
            }

        [Writer]
        void DestroyControllerInstance()
            {
            if (controllerInstance.Any())
                controllerInstance.Single().Close();
            controllerInstance = Maybe<DeviceController>.Empty;
            }

        [Writer]
        public void GoOffline(Guid clientId)
            {
            log.Info($"Go offline for client {clientId}");
            ClientStatus client = null;
            try
                {
                client = Clients.Single(p => p.Equals(clientId));
                }
            catch (InvalidOperationException e)
                {
                var message = $"Attempt to go offline by unecognized client {clientId}";
                log.Error(e, message);
                //ThrowOnUnrecognizedClient(clientId, e, message);
                }

            client.Online = false;
            RaiseClientStatusChanged();
            if (OnlineClientCount == 0)
                {
                log.Warn($"The last client has gone offline - closing connection");

                if (controllerInstance.Any())
                    {
                    var controller = controllerInstance.Single();
                    controller.Close();
                    }
                }
            }


        /// <summary>
        ///     Determines whether the client with the specified ID is registered.
        /// </summary>
        /// <param name="clientId">The client unique identifier.</param>
        /// <returns><c>true</c> if the client is connected; otherwise, <c>false</c>.</returns>
        [Reader]
        public bool IsClientRegistered(Guid clientId) => Clients.Any(p => p.Equals(clientId));

        /// <summary>
        ///     Gets a new unique client identifier.
        /// </summary>
        /// <returns>Guid.</returns>
        [Writer]
        public Guid RegisterClient(string name = null)
            {
            var id = Guid.NewGuid();
            var status = new ClientStatus {ClientId = id, Name = name ?? id.ToString(), Online = false};
            Clients.Add(status);
            RaiseClientStatusChanged();
            return id;
            }

        [Writer]
        public void UnregisterClient(Guid clientId)
            {
            log.Info($"Unregistering client {clientId}");
            var previousClientCount = RegisteredClientCount;
            try
                {
                Clients.Remove(Clients.Single(p => p.Equals(clientId)));
                RaiseClientStatusChanged();
                }
            catch (InvalidOperationException e)
                {
                var message = $"Attempt to unregister unknown client {clientId}";
                log.Error(e, message);
                //ThrowOnUnrecognizedClient(clientId, e, "Attempt to unregister an unknown client");
                }
            if (previousClientCount == 1 && RegisteredClientCount == 0)
                {
                DestroyControllerInstance();
                Server.TerminateLocalServer();
                }
            }

        /// <summary>
        ///     Throws an ASCOM.<see cref="T:ASCOM.InvalidOperationException" /> with information about registered clients.
        /// </summary>
        /// <param name="clientId">The client identifier causing the original exception.</param>
        /// <param name="e">The original (inner) exception.</param>
        /// <param name="message">The error message.</param>
        [ContractAnnotation("=>halt")]
        void ThrowOnUnrecognizedClient(Guid clientId, Exception e, string message)
            {
            var ex = new ASCOM.InvalidOperationException($"Connection Manager: {message}", e);
            ex.Data["RegisteredClients"] = Clients;
            ex.Data["UnknownClient"] = clientId;
            log.Error(ex, $"Client not found: {clientId}");
            throw ex;
            }
        }
    }