CoAP.NET Release Notes
======================

changes in 1.1.0
----------------

* [added] clean-up mechanism for blockwise transfer state
* [added] filter when notifying observers
* [added] send and receive events to IEndPoint
* [added] support for cancelling reject messages
* [added] support for cancelling blockwise transfer
* [added] support for blockwise transfers with NON
* [updated] rename obsoleted events and properties in CoAP Message
* [updated] mark as CLSCompliant
* [updated] lazy initializing lock in a request to avoid locks if possible
* [updated] collection classes for .NET 2.0
* [fixed] potential stack overflow in underlying UDP channel
* [fixed] Message ID conflict
* [fixed] incomplete cleanup when reregister a observation
* [fixed] race condition when cancelling retransmission
* [fixed] for parentless resources
* [fixed] RTT for blockwise transfers
* [fixed] success range for code constants

changes in 1.0.0
----------------

* [added] support for RFC7252!
* [improved] redefine compile symbols
* [improved] rename events and properties in CoAP Message
* [improved] allow random block request
* [improved] separate CoAP.Proxy namespace
* [improved] update Common.Logging to v3.0.0
* [fixed] incorrect link format serialization
* [fixed] null reference in CoAPClient.Discover()
* [fixed] a lot of others

changes in 0.18
---------------

* [added] support for CoAP-18
* [added] new layers and stack structure
* [added] new server and client APIs
* [added] datagram channels for data transmission
* [added] full configuration
* [improved] UDP transmission with SocketAsyncEventArgs for .NET 4+
* [improved] threading with TPL for .NET 4+
* [improved] logging with Common.Logging
* [fixed] lots of bugs

changes in 0.13.4
-----------------

* [fixed] potential breaks in UDP transmission
* [improved] diff assembly title with draft version

changes in 0.13.3
-----------------

* [added] Request.SequenceTimeout to override overall timeout in
  TokenLayer
* [fixed] incorrect match of tokens in TokenManager

changes in 0.13.2
-----------------

* [added] ICommunicator to represent communicators
* [added] ICoapConfig to pass initial variables (refs #8)
* [added] HTTP/CoAP proxy (experimental)
* [added] build for .NET 4.0
* [improved] dispatch requests with thread pool in LocalEndPoint
* [improved] only timeout requests if SequenceTimeout is greater
  than 0 in TokenLayer
* [improved] move resources to separate namespace Resources, and
  add a TimerResource for timed observable resources.

Changes in 0.13.1
------------------

* add timeout and max retransimit to each message
* enable log levels
* fix null reference to next block in TransferLayer

Changes in 0.13
----------------

* update to CoAP-13
* support drafts switching

Version 0.08
-----------

* update to CoAP-08
* support both IPv6/IPv4
