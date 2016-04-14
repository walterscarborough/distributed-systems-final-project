# Distributed Test Environment Tool

#### Created by Jon Beverly, Maurice Roth-Miller, and Walter Scarborough

Distributed Test Environment (DTE) is a distributed systems testing tool that gives developers the ability to inject faults into messages passed in a distributed system.

The DTE works by intercepting messages bound for a port.  Developers can then inject faults into messages, which are forwarded to the recipient node.  The process of intercepting messages allows developers the freedom to use any programming language in their target implementation without modification, and makes recreating faults trivial.

![DTE Screenshot](../blob/master/documentaton/images/dte-screenshot.png?raw=true "DTE Screenshot")

## Why DTE?

DTE addresses the weaknesses in other testing approaches:

* Users can select the fault(s) that they are interested in testing.
* No modifications to target programs are necessary.
* Any program that uses TCP sockets is supported - there are no constraints on implementation languages.

## Design

The DTE is a stand-alone application that runs on Microsoft Windows 10 platform.  Systems under test can be written in any programming language and only need to open ports to the computer where the DTE application is located.  The DTE is also non-invasive because systems under test do not need to be modified for testing.  Because the DTE is a standalone application, it can be installed on the developer’s desktop and does not require the system under test to be published to a testbed for testing.

## Fault Injection

Fault injection is the main purpose and motivation behind our design of the DTE, and is implemented in a single model in the source code, FaultInjectionModel.  The DTE can inject the following faults: 

* delay for user provided milliseconds
* lose message
* duplicate message
* corrupt message
* create out of order messages.

Corrupt messages are created by exclusive-or of bytes with 0x55.  Adding new fault types to the DTE requires modifying only the FaultInjectionModel. 

Faults are injected on the receiving port when the DTE catches the message.  When the DTE receives a message, it logs the incoming message, checks the enabled faults for that port, applies the faults, logs the new message, and forwards that message to the system under test.

## More Information

* [DTE Presentation](/documentation/slides.pptx)
* [DTE Poster](/documentation/poster.png)

Contributions are welcome! Feel free to submit a pull request.
