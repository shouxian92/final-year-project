matric = "U12345678A";
pi_ip = "192.168.1.8";
cr_ip = "192.168.1.9";

/*

Sub-resources under this particular instance 
of the matric card reader to allow the updating of ip addresses

*/

var sub1 = new JavaScriptResource("pi_ip");
app.root.add(sub1);

// that accepts GET requests
sub1.onget = function(request) {
  // to configure the threshold
  request.respond(2.05, "IP address of rpi: " + pi_ip);
};

// that accepts PUT requests
sub1.onput = function(request) {
  // to configure the threshold
  pi_ip = request.requestText;
  request.respond(2.05, "IP address now changed to " + pi_ip);
};

// JS resource for card reader's IP address
var sub2 = new JavaScriptResource("cr_ip");
app.root.add(sub2);

// that accepts GET requests
sub2.onget = function(request) {
// to configure the threshold
request.respond(2.05, "Card reader IP address: " + cr_ip);
};

// that accepts PUT requests
sub2.onput = function(request) {
cr_ip = request.requestText;
request.respond(2.05, "IP address now changed to " + cr_ip);
};

/*

Main application logic

*/

// get the last matric card that was read"/"
app.root.onget = function(request) {

// that returns CoAP's "2.05 Content" with payload
request.respond(2.05, matric);
};

app.root.onpost = function(request) {
// replace matric card with payload
matric = request.requestText;

display_request = new CoapRequest();
// updates the LED display to display the new matric card number
display_request.open("POST", "coap://" + pi_ip + ":5683/display_res", true);
display_request.send(matric);

// lights up the red LED
red_led_request = new CoapRequest();
red_led_request.open("PUT", "coap://" + pi_ip + ":5683/green_led_res", true);
red_led_request.send("1");

// uncomment this to light up the blue LED
// var blue_led_request = new CoapRequest();
// blue_led_request.open("PUT", "coap://" + pi_ip + ":5683/blue_led_res", true);
// blue_led_request.send("1");

// uncomment this to send the request back to the card reader app
cr_request = new CoapRequest();
cr_request.open("POST", "coap://" + cr_ip + ":5683/lookup-matric", true);
cr_request.send(matric);

request.respond(2.05, "Matric number now changed to " + matric);
};