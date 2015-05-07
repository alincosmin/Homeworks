var PingPong = {
    main: { width: 1350, height: 630 },

    info: {
         positions: {
             playerOne: { x: 50, y: 50 }, 
             playerTwo: { x: 1300, y: 50 }
         }, 
         scores: {
             playerOne: 0,
             playerTwo: 0
         }
    },

    updateScores: function (scoreA, scoreB) {
        var self = this;
        self.info.scores.playerOne = scoreA;
        self.info.scores.playerTwo = scoreB;
    },

    movePlayers: function(ax, ay, bx, by) {
        var self = this;
        self.info.positions.playerOne.x = ax;
        self.info.positions.playerOne.y = ay;
        self.info.positions.playerTwo.x = bx;
        self.info.positions.playerTwo.y = by;
    },

    ball: function (sendMove, sendScores) {
        var self = this;
        var canvas = document.getElementById('canvas'),
            ctx,
            x = 65, /* Starting Xposition */
            y = 110, /* Starting Yposition */
            dx = 10,
            dy = 0,
            doy = 10,
            dty = 20,
            impact,
            flag = 0,
            aKey = true,
            keys = [],
            pty = 50,
            a,
            c = 0,
            lastKey = 0;

        canvas.setAttribute('width', self.main.width);
        canvas.setAttribute('height', self.main.height);

        function circle(x, y, r) {
            ctx.beginPath();
            ctx.arc(x, y, r, 0, Math.PI * 2, true);
            ctx.fill();
        }

        function rect(x, y, w, h) {
            ctx.beginPath();
            ctx.rect(x, y, w, h);
            ctx.closePath();
            ctx.fill();
            ctx.strokeStyle = '#fff';
            ctx.lineWidth = '5px';
            ctx.beginPath();
            ctx.moveTo(w / 2, 0);
            ctx.lineTo(w / 2, h);
            ctx.stroke();
            ctx.closePath();
        }

        function p1Rect(pox, poy, pow, poh) {
            ctx.beginPath();
            ctx.rect(pox, poy, pow, poh);
            ctx.closePath();
            ctx.fill();
        }

        function p1Score(ox, oy) {
            ctx.font = '100px Arial';
            ctx.strokeText(self.info.scores.playerOne, ox, oy);
        }

        function p2Rect(ptx, pty, pow, poh) {
            ctx.beginPath();
            ctx.rect(ptx, pty, pow, poh);
            ctx.closePath();
            ctx.fill();
        }

        function p2Score(tx, ty) {
            ctx.font = '100px Arial';
            ctx.strokeText(self.info.scores.playerTwo, tx, ty);
        }

        function clear() {
            ctx.clearRect(0, 0, self.main.width, self.main.height);
        }

        onKeyDown = onKeyUp = function (event) {
            event.preventDefault();
            keys[event.keyCode] = event.type == 'keydown';
            if (keys[32]) { // spacebar
                if (flag == 0) {
                    flag = 1;
                }
            }
            if (keys[38]) { // 
                if (lastKey >= 0) lastKey++;
                else lastKey = 0;

                if (self.info.positions.playerOne.y - doy > -15) {
                    if (flag == 0 && x == 65) {
                        self.info.positions.playerOne.y -= doy + lastKey;
                        y -= doy + lastKey;
                    } else {
                        self.info.positions.playerOne.y -= doy + lastKey;
                    }
                }
            }
            if (keys[40]) { // down
                if (lastKey <= 0) lastKey--;
                else lastKey = 0;

                if (self.info.positions.playerOne.y + doy < self.main.height - 105) {
                    if (flag == 0 && x == 65) {
                        self.info.positions.playerOne.y += doy - lastKey;
                        y += doy - lastKey;
                    }
                    else {
                        self.info.positions.playerOne.y += doy - lastKey;
                    }
                }
            }

            sendMove(0, self.info.positions.playerOne.x, self.info.positions.playerOne.y);
        }

        function calibrateAngle(impact) {
            if (impact < 59 && impact > 49) {
                dy = -2;
            }
            else if (impact < 50 && impact > 39) {
                dy = -4;
            }
            else if (impact < 40 && impact > 29) {
                dy = -6;
            }
            else if (impact < 30 && impact > 19) {
                dy = -7;
            }
            else if (impact < 20 && impact > 9) {
                dy = -8;
            }
            else if (impact < 10 && impact > -1) {
                dy = -9;
            }
            else if (impact > 62 && impact < 70) {
                dy = 2;
            }
            else if (impact > 69 && impact < 80) {
                dy = 4;
            }
            else if (impact > 79 && impact < 90) {
                dy = 6;
            }
            else if (impact > 89 && impact < 100) {
                dy = 7;
            }
            else if (impact > 99 && impact < 110) {
                dy = 8;
            }
            else if (impact > 109 && impact < 121) {
                dy = 9;
            }
            else {
                dy = 0;
            }
        }

        function checkCollision() {
            var id = ctx.getImageData(x, y, 12, 12),
                px = id.data;
            for (var i = 0; i < px.length; i += 4) {
                if (px[i + 1] == 58) {
                    c = 1;
                    if (x < (self.main.width / 2)) {
                        impact = y - poy;
                        calibrateAngle(impact);
                    }
                    else {
                        impact = y - pty;
                        calibrateAngle(impact);
                    }
                }
            }
        }

        function init() {
            ctx = canvas.getContext("2d");
            return setInterval(draw, 15);
        }

        function addScore() {
            /* End point if off the edge of screen */
            if (x + dx > self.main.width) {
                flag = 0;
                self.info.positions.playerOne.x = 50;
                self.info.positions.playerOne.y = 50;
                x = 65; /* Starting Xposition */
                y = 110; /* Starting Yposition */
                dy = 0;

                sendScores(self.info.scores.playerOne + 1, self.info.scores.playerTwo);
            }
            if (x + dx < 0) {
                flag = 0;
                self.info.positions.playerTwo.x = self.main.width - 50;
                self.info.positions.playerTwo.y = 50;
                x = self.main.width - 61; /* Starting Xposition */
                y = 110; /* Starting Yposition */
                dy = 0;
                dx = -dx;

                sendScores(self.info.scores.playerOne, self.info.scores.playerTwo + 1);
            }
        }

        function draw() {
            clear();
            ctx.fillStyle = '#eee';
            rect(0, 0, self.main.width, self.main.height);
            ctx.fillStyle = 'rgba(114,58,58,1)';
            ctx.strokeStyle = 'rgba(120,88,88,1)';
            p1Rect(self.info.positions.playerOne.x, self.info.positions.playerOne.y, 4, 120);
            p2Rect(self.info.positions.playerTwo.x, self.info.positions.playerTwo.y, 4, 120);
            ctx.strokeStyle = '#fff';
            p1Score((self.main.width / 4) - 30, self.main.height / 2);
            p2Score(((self.main.width / 4) * 3) - 50, self.main.height / 2);
            a == 1 ? ctx.fillStyle = "#444444" : ctx.fillStyle = "#b3b3b3";
            circle(x, y, 10);

            addScore();
            flag != 0 ? checkCollision() : '';

            if (c == 1) {
                dx = -dx;
                c = 0;
            }

            if (flag == 0) { }
            if (flag == 1) {
                x += dx; y += dy;
            }

            if (y + dy > self.main.height || y + dy < 0) {
                dy = -dy;
            }
        }

        init();
        window.addEventListener('keydown', onKeyDown, true);
        window.addEventListener('keyup', onKeyUp, true);
    }
};