/*******************************************************************************
 ******************************************************************************/
// Crucia to import the CoAPRequest class here

// a handler for GET requests to "/"
app.root.onget = function(request) {
  start = 0;
  loops = 800;
  firstNumber = 0;
  for (i = start; i < loops; i++) {
    var client = new CoapRequest();
    var instance_name = "matric_reader_"+i;
    dump("Creating " + instance_name);
    client.open("POST", "coap://127.0.0.1:5683/install/matricCardReader", false);
    client.send("name = " + instance_name);

    var starter = new CoapRequest();
    starter.open("POST", "coap://127.0.0.1:5683/apps/instances/matric_reader_"+i, false);
    // onload function is used here so that after the start request is done, we are able to
    // change variables on the app instantly
    // we re-use the 'client' variable which has no overriden onload method so that 
    // we do not enter an infinite recursion
    /*
    starter.onreadystatechange = function() {
      if (this.readyState==this.DONE) {
        client.open("PUT", "coap://127.0.0.1:5683/apps/running/matric_reader_"+i+"/cr_ipaddress", false);
        secondNumber = i % 256 + 1;

        if(secondNumber == 1)
        firstNumber += 1;

        ip_config = "192.168."+firstNumber+"."+secondNumber;
        dump("Sending put request for ip address: " + ip_config);
        client.send(ip_config);
      }
    }*/

    starter.send("start");
  }

// that returns CoAP's "2.05 Content" with payload
request.respond(2.05, "Request for creation of a "+ loops +" instances of matric_card_reader started");
};