const app = require('./src/app');
let port = process.env.PORT || 5000;

app.listen(port, () => {
   console.log(`web api running http://localhost:${port}`);
});