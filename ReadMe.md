# ASCOM drivers for Digital DomeWorks - 2018 Reboot #

## TL;DR ##

<button type="button" class="btn btn-secondary">[Downloads][download]</button> |
<button type="button" class="btn btn-secondary">[Source Code][git]</button> |
<button type="button" class="btn btn-secondary">[Report a Bug][bugs]</button> |
<button type="button" class="btn btn-secondary">[Project Home Page][project-home]</button>

## Overview ##

In 2006, Tigra Astronomy was commissioned buy Technical Innovations to produce an ASCOM driver for their observatory automation product known as Digital DomeWorks. The project served well for 12 years but here we are in 2018 and it was looking a bit tired. In the intervening years, the ASCOM LocalServer (hub) pattern was created; we created our Reactive Communications for ASCOM library; and we learned a lot about the art of software development based on experience of producing more than 10 commercial ASCOM drivers for telescopes, domes, focusers, rotators, switches and weather safety systems.

But we started to experience problems with the old driver and we wre not happy with the robustness in operation nor the code quality. We decided it was time for a new start. So we have produced the 2018 Reboot version, sporting the following features:

- ASCOM LocalServer/Hub that accepts multiple client connections.
- ASCOM Dome driver, implementing the IDomeV2 interface.
- ASCOM Switch driver, implementing the ISwitchV2 interface.
  Allows applications to control
  the user output pins and therefore the T.I. Remote Power Module.
- Low level code is based on a state machine that tracks the
  state of the hardware. Invalid operations are simply ignored.
  This improves the robustness of the driver.
- Reliable shutter state upon connection. If the shutter position is unknown when
   the driver connects, then it can optionally be forced to close to establish a
   known starting state.
- Hardware simulator built in. To test the driver without hardware, simply check
   the "use simulator" option in the setup dialog.
- New graphical user interface shows the current state of the hardware.
- Open and Close buttons in the status display allow convenient opening
  and closing.
- Easy access to the Setup Dialog from the status display.
- Display of the number of active client connections.
- Fully asynchronous operation except where the ASCOM Standards indicate
  otherwise.
- Event driven code means that the client application never has to wait
  for an answer, the latest data is alaways available immediately without
  having to send a command and wait for the response. This means we are
  never blocking the user interface and we don't slow down applications.
- Use of the [Reactive Communications for ASCOM][RxAscom] library means
  that the driver is thread-safe for use in multi-threaded applications
  such as SGP.

## Simulator ##

The driver now incorporates the hardware simulator that we developed to assist with our unit testing. The simulator can work in `real time` or `quick time`. In `real time` mode the simulator tries to be as realistic as possible, simulating the timing characteristics of the Digital DomeWorks hardware. In `quick time` mode, everything is done as fast as possible.

The simulator can come in very handy if you need to test software without being close to the actual hardware. Previous versions required a serial port but the new version uses an in-memory communications channel built on the technology in our [Reactive Communications][RxAscom] library. To use the simulator, all that is needed is to check the checkbox in the setup dialog.
[Simulator checkbox here]

## Power Automation ##

Previous versions had some clunky command line utilities and _Custom Actions_ for controlling the user output pins. These pins are typically used with the _Remote Power Module_. Now, we provide an ASCOM Switch driver making this much easier and more discoverable and usable by applications. And, because the new driver is a LocalServer Hub, both drivers can be in use by multiple clients simultaneously and everything will work.

## Obtaining and Installing the Driver ##

The driver may be downloaded from: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks/downloads/

The source code is available at: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks

The download is a zip file containing 4 installers. There are `Debug` and `Release` builds for each of `x86` (32-bit) and `x64` (64-bit) architectures. End users should normally use the `Release` build and install the Release configuration that matches their machine architecture: `x86` for 32-bit systems and `x64` for 64-bit systems. The installer checks this and will not allow the wrong architecture to be installed.

The `Debug` configurations contain debugging sybols and produce *copious amounts* of diagnostic output and therefore run relatively slowly. ***Debug builds are not recommended for normal use*** and should only be installed when troubleshooting with a debugger is required.

## Open Source ##

In August 2015, Tigra Astronomy took the decision to open-source the project and we have continued with that policy in the 2018 reboot. The source code is available from a BitBucket Git repository. The code is being made available under the MIT license which is about as permissive as it gets. Basically, anyone can do anything at all with the software with no strings attached, and we are not liable for the consequences, whatever they are.

End users should normally only install the latest release build. Beta builds are likely to contain bugs and are not supported for production use. Integration builds contain up-to-the-minute code changes but might not be production quality so are not supported under any circumstances.

## Bug Reports and Feature Requests ##

Hopefully there will be few problems, but if you do find any, or would like to request a new feature or provide other feedback, then please use our [official public/anonymous bug tracker][bugs]. Unless it's in our bug tracker, we haven't received it and you cannot expect any response.


[bugs]: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks/issues?status=new&status=open "BitBucket Public Issue Tracker"
[RxAscom]: http://tigra-astronomy.com/reactive-communications-for-ascom "Reactive ASCOM project home page"
[download]: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks/downloads/ "Download the drivers"
[git]: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks "Get the source code"
[project-home]: http://tigra-astronomy.com/ascom-drivers-for-digital-domeworks "ASCOM Drivers for Digital Domeworks, 2018 reboot"
