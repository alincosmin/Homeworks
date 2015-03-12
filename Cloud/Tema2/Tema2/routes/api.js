var express = require('express');
var router = express.Router();

var contacts = [
    {
        phone: '0747123',
        name: 'Andrei',
        age: 22
    },
    {
        phone: '0732212',
        name: 'Cristi',
        age: 21
    },
    {
        phone: '0722133',
        name: 'Bogdan',
        age: 23
    }
];

var sentMessages = [];

router.get('/contactlist', function (req, res) {
    res.send(contacts);
});

router.get('/sent', function(req, res) {
    res.send(sentMessages);
});

router.get('/sent/:id', function(req, res) {
    var retmsg = null;

    if (req.params.id < sentMessages.length && req.params.id > 0) {
        retmsg = sentMessages[req.params.id];
    }
    if (retmsg) {
        res.send(retmsg);
    } else {
        res.send(404, "Message not found");
    }
});

router.post('/sendmessage/:phone', function(req, res) {
    var msg = {
        id : sentMessages.length,
        phone: req.params.phone,
        message: req.body.message,
        date: new Date()
    };

    sentMessages.push(msg);
    res.location('Location: /api/sent/' + msg.id);
    res.send(201, msg);
});

module.exports = router;