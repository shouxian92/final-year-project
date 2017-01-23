var test = require('test_lib');
app.root.onget = function(request) {
    request.respond(2.05, test.getID());
};
