/*
var express = require('express');
var app = express();
var fs = require("fs");

matric = "U12345678A";
android_ip = "192.168.1.4"
cr_ip = "192.168.1.8"

// var sub1 = new JavaScriptResource("android_ipaddress");
// app.root.add(sub1);

// that accepts GET requests
app.get('/matric_reader/android', function(req, res) {
    res.end("IP address of android device: " + android_ip);
})

// that accepts PUT requests
app.put('/matric_reader/android', function(req, res) {
  android_ip = req.params.android_ip;
  res.end( "IP address of android now changed to " + android_ip);
})

// that accepts GET requests
app.get('/matric_reader/cr_ip', function(req, res) {
    res.end("IP address of card reader: " + cr_ip);
})

// that accepts PUT requests
app.put('/matric_reader/cr_ip', function(req, res) {
  cr_ip = req.params.cr_ip;
  res.end( "IP address of card reader now changed to " + android_ip);
})

// get the last matric card that was read"/"
app.get('/matric_reader', function (req, res) {
  res.end(matric);
})

app.put('/matric_reader', function(req, res) {
  // replace matric card with payload
  matric = req.params.matric;

  // TODO: send a post request to a display device
  res.end("Matric number now changed to " + matric);
})
*/ 
process.on('message', function(message) {
  // console.log('[child] received message from server:', message);
  process.send({
      child   : process.pid,
      result  : message + 1
  });
  process.disconnect();
  /*setTimeout(function() {
    process.send({
      child   : process.pid,
      result  : message + 1
    });
    process.disconnect();
  }, (0.5 + Math.random()) * 5000);*/
});