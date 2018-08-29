ASCOM Drivers for Digital Domeworks, 2018 Reboot
================================================

Instructions for Beta Testers
-----------------------------

This build is beta quality, which means it most likely contains
bugs, although it shoul dbe mostly feature-complete. You test
this software entirely at your own risk.

About the 2018 Reboot
---------------------

This version (7.0) of the Digital Domeworks ASCOM driver is a
ground-up rewrite, using all the new skills and knowledge we
have acquired since the original version was started almost
12 years ago.

The 2018 driver is an ASCOM LocalServer, meaning it can accept
multiple concurrent connections from client applications. Two
ASCOM drivers are included:

1. ASCOM Dome driver, implementing the `IDomeV2` interface. This
   driver controls dome operations such as rotation, opening and
   closing the shutter.
2. ASCOM Switch driver, implementing the `ISwitchV2` interface.
   This driver allows control of the _user output pins_ which
   works with the _Technical Innovations Remote Power Module_.

Drivers appear in the ASCOM Chooser as _Digital Domeworks 2018_.

This driver uses different identifiers to the original Digital
Domeworks driver so can safely be installed side-by-side with it.
There is no need to uninstall the old driver.

Installation - Important, Read Carefully
------------

**Currently there is no installer, so before attempting to use
the driver you must perform a one-time registration**.

Simply unzip the package into a folder, which can be in any convenient location such as on the Desktop, and proceed as follows:

- Open a command prompt such as `Windows PowerShell` or `cmd.exe`.
- Change directory to the folder where you have saved the program files.
- Issue the following command: `TA.DigitalDomeworks.Server.exe /register`
- You should see the security prompt as the program requests
  elevated permissions. You must allow this operation.
- You're done. The driver should now appear in the ASCOM Chooser.

The release version of the driver will have an installer that automates this process, but it is not completed yet.

Reporting Bugs and Feedback
---------------------------

Please report all bugs, issues, feedback and feature requests at the [official public issue tracker][issues].

[issues]: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks/issues?status=new&status=open "Public Issue Tracker"