var express = require('express');
var router = express.Router();

/* GET home page. */

router.get('/search/:term', function(req, res) {
    var response = { 'term': req.params.term, 'answer': 'you searched for something' };
    res.send(response);
});

module.exports = router;