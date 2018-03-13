using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NLog.Fluent;
using TA.Ascom.ReactiveCommunications.Diagnostics;

namespace TA.DigitalDomeworks.DeviceInterface
{
    static class ObservableExtensions
        {
        /// <summary>
        /// Extracts azimuth encoder ticks from a source sequence and emits
        /// the encoder values as an observable sequence of integers.
        /// </summary>
        /// <param name="source"></param>
        public static IObservable<int> AzimuthEncoderTicks(this IObservable<char> source)
            {
            const string azimuthEncoderPattern = @"^P(?<Azimuth>\d{1,4})[^0-9]";
            var azimuthEncoderRegex =
                new Regex(azimuthEncoderPattern, RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            var buffers = source.Publish(s => s.BufferByPredicates(p => p == 'P', q => !char.IsDigit(q)));
            var azimuthValues = from buffer in buffers
                                let message = new string(buffer.ToArray())
                                let patternMatch = azimuthEncoderRegex.Match(message)
                                where patternMatch.Success
                                let azimuth = int.Parse(patternMatch.Groups["Azimuth"].Value)
                                select azimuth;
            return azimuthValues.Trace("EncoderTicks");
            }

        public static IObservable<IList<char>> BufferByPredicates(this IObservable<char> source,
            Predicate<char> bufferOpening, Predicate<char> bufferClosing)
            {
            return source.Buffer(source.Where(c => bufferOpening(c)), x => source.Where(c => bufferClosing(c)));
            }
        }
    }
