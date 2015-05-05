var PingPong = {
    ball: function () {
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
            scores = { playerOneScore: 0, playerTwoScore: 0 },
            aKey = true,
            keys = [],
            WIDTH = 1350,
            HEIGHT = 630,
            pox = 50,
            poy = 50,
            ptx = WIDTH - 50,
            pty = 50,
            a,
            c = 0,
            lastKey = 0;

        canvas.setAttribute('width', WIDTH);
        canvas.setAttribute('height', HEIGHT);

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
            ctx.strokeText(scores.playerOneScore, ox, oy);
        }

        function p2Rect(ptx, pty, pow, poh) {
            ctx.beginPath();
            ctx.rect(ptx, pty, pow, poh);
            ctx.closePath();
            ctx.fill();
        }

        function p2Score(tx, ty) {
            ctx.font = '100px Arial';
            ctx.strokeText(scores.playerTwoScore, tx, ty);
        }

        function clear() {
            ctx.clearRect(0, 0, WIDTH, HEIGHT);
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

                if (poy - doy > -15) {
                    if (flag == 0 && x == 65) {
                        poy -= doy + lastKey;
                        y -= doy + lastKey;
                    } else {
                        poy -= doy + lastKey;
                    }
                }
            }
            if (keys[40]) { // down
                if (lastKey <= 0) lastKey--;
                else lastKey = 0;

                if (poy + doy < HEIGHT - 105) {
                    if (flag == 0 && x == 65) {
                        poy += doy - lastKey;
                        y += doy - lastKey;
                    }
                    else {
                        poy += doy - lastKey;
                    }
                }
            }
        }

        function checkCollision() {
            var id = ctx.getImageData(x, y, 12, 12),
                px = id.data;
            for (var i = 0; i < px.length; i += 4) {
                if (px[i + 1] == 58) {
                    c = 1;
                    if (x < (WIDTH / 2)) {
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

        function init() {
            ctx = canvas.getContext("2d");
            return setInterval(draw, 15);
        }

        function addScore() {
            /* End point if off the edge of screen */
            if (x + dx > WIDTH) {
                scores.playerOneScore++;
                flag = 0;
                pox = 50;
                poy = 50;
                x = 65; /* Starting Xposition */
                y = 110; /* Starting Yposition */
                dy = 0;
            }
            if (x + dx < 0) {
                scores.playerTwoScore++;
                flag = 0;
                ptx = WIDTH - 50;
                pty = 50;
                x = WIDTH - 61; /* Starting Xposition */
                y = 110; /* Starting Yposition */
                dy = 0;
                dx = -dx;
            }
        }

        function draw() {
            clear();
            ctx.fillStyle = '#eee';
            rect(0, 0, WIDTH, HEIGHT);
            ctx.fillStyle = 'rgba(114,58,58,1)';
            ctx.strokeStyle = 'rgba(120,88,88,1)';
            p1Rect(pox, poy, 4, 120);
            p2Rect(ptx, pty, 4, 120);
            ctx.strokeStyle = '#fff';
            p1Score((WIDTH / 4) - 30, HEIGHT / 2);
            p2Score(((WIDTH / 4) * 3) - 50, HEIGHT / 2);
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

            if (y + dy > HEIGHT || y + dy < 0) {
                dy = -dy;
            }
        }

        init();
        window.addEventListener('keydown', onKeyDown, true);
        window.addEventListener('keyup', onKeyUp, true);
    }
};