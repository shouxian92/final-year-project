package com.shouxian.californiumtestapp;
/*******************************************************************************
 * Copyright (c) 2015 Institute for Pervasive Computing, ETH Zurich and others.
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
 *    Matthias Kovatsch - creator and main architect
 ******************************************************************************/

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;

import org.eclipse.californium.core.CoapResource;
import org.eclipse.californium.core.CoapServer;
import org.eclipse.californium.core.server.resources.CoapExchange;

public class ServerService extends Service {

    CoapServer server;

    @Override
    public void onCreate() {
        this.server = new CoapServer();

        // attempt to bind a new endpoint to an IP address
        // over CoAP
        /*
        try {
            // 224.0.1.187 is the 'All CoAP Nodes' address and is
            // common to all CoAP services who wants to make themselves discoverable
            InetAddress addr = InetAddress.getByName("224.0.1.187");
            InetSocketAddress bindToAddress = new InetSocketAddress(addr, 5683);
            CoapEndpoint multicast = new CoapEndpoint(bindToAddress);
            server.addEndpoint(multicast);

            // binds localhost as well
            addr = InetAddress.getByName("127.0.0.1");
            bindToAddress = new InetSocketAddress(addr, 5683);
            multicast = new CoapEndpoint(bindToAddress);
            server.addEndpoint(multicast);
        } catch (UnknownHostException e) {
            // happens when the host cannot be resolved
        }*/

        server.add(new HelloWorldResource());
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {

        server.start();

        return START_STICKY;
    }

    @Override
    public void onDestroy() {
        server.destroy();
    }

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        throw new UnsupportedOperationException("Not yet implemented");
    }

    class HelloWorldResource extends CoapResource {

        public HelloWorldResource() {

            // set resource identifier
            super("hello");

            // set display name
            getAttributes().setTitle("Hello-World Resource");
        }

        @Override
        public void handleGET(CoapExchange exchange) {

            // respond to the request
            exchange.respond("Hello Android!");
        }
    }
}