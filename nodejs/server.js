var express = require('express');
var app = express();

app.use(express.static('public'));

app.get('/index.htm', function (req, res) {
   res.sendFile( __dirname + "/" + "index.htm" );
})

app.get('/process_get', function (req, res) {
   // creates a coap object and performs a coap request
   const coap  = require('coap')
    , coap_req   = coap.request('coap://localhost:5683/apps/running/hello')

   coap_req.on('response', function(coap_res) {
      // converts the buffered response into a string
      // optional
      var textChunk = coap_res.payload.toString('utf8');

      // prints the response out to the user
      console.log(coap_res.payload);
      res.end(coap_res.payload);
   })

   coap_req.end()

})

// initialise the server object and start listening on a given port
var server = app.listen(8081, function () {
   var host = server.address().address
   var port = server.address().port
   console.log("Example app listening at http://%s:%s", host, port)
})