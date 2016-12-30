/*******************************************************************************
 * Copyright (c) 2014 Institute for Pervasive Computing, ETH Zurich and others.
 * 
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * and Eclipse Distribution License v1.0 which accompany this distribution.
 * 
 * The Eclipse Public License is available at
 *    http://www.eclipse.org/legal/epl-v10.html
 * and the Eclipse Distribution License is available at
 *    http://www.eclipse.org/org/documents/edl-v10.html.
 * 
 * Contributors:
 *    Martin Lanter
 ******************************************************************************/

address = "192.168.1.4";

var sub1 = new JavaScriptResource("config");
app.root.add(sub1);
// that accepts PUT requests
sub1.onput = function(request) {
// to configure the threshold
address = request.requestText;
request.respond(2.05, "IP address now changed to " + address);
};


// a handler for GET requests to "/"
app.root.onget = function(request) {

var req = new CoapRequest();

// request the PIR sensor resource of a mote via CoAP
// and set the type to a NON request
req.open("GET", "coap://"+ address +":5683/.well-known/core",
false /*synchronous*/);
// with a application/json response
req.setRequestHeader("Accept", "application/json");
req.send(); // blocking
// and log it to the console after send() returns
// app.dump(req.responseText);

// that returns CoAP's "2.05 Content" with payload
request.respond(2.05, req.responseText);
};