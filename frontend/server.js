var express = require('express');
const path = require('path');
var app = express();
var PORT = 3000;

/* serves the static files in the ./frontend folder */
app.use(express.static(__dirname));

/* serves the index.html file for any route */
app.get('*', (req, res, next) => {
res.sendFile(path.join(__dirname, '/index.html'));
});

/* listens on port 3000 */
app.listen(PORT, () => {
console.log(`Listening on port ${PORT}`);
});

/*                    Mihael */
