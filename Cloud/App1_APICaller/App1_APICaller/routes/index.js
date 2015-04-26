var express = require('express');
var router = express.Router();

/* GET home page. */

router.get('/search/:term', function(req, res) {
    var response = { 'term': req.params.term, 'answer': 'you searched for something' };
    res.send(response);
});

router.get('/getLetters', function(req, res) {
    var response = "abcdefghijklmnopqrstuvwxyz";

    for (var i = 0; i < response.length; i++) {
        setTimeout(function() {
            res.send(200, 1);
        }, 1000);
    }

});

module.exports = router;