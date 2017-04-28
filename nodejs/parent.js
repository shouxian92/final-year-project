// http://stackoverflow.com/questions/20004935/best-way-to-execute-parallel-processing-in-node-js

var child_process = require('child_process');

var numchild  = 500; // number of instances to spawn // require('os').cpus().length;
var done      = 0;

// https://blog.tompawlak.org/measure-execution-time-nodejs-javascript
var start = new Date();
var hrstart = process.hrtime();

for (var i = 0; i < numchild; i++){
  var child = child_process.fork('./child');
  child.send((i + 1) * 1000);
  child.on('message', function(message) {
    // console.log('[parent] received message from child:', message);
    done++;
    if (done === numchild) {
      // console.log('[parent] received all results');
      var end = new Date() - start,
      hrend = process.hrtime(hrstart);

      console.info("Execution time: %dms", end);
      console.info("Execution time (hr): %ds %dms", hrend[0], hrend[1]/1000000);
    }
  });
}