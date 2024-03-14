const express = require('express');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const PORT = process.env.PORT || 3030;

// Middleware
app.use(bodyParser.json());
app.use(cors())

// Заглушки для API
app.get('/start', (req, res) => {
    res.send([
        "Должность 1",
        "Должность 2"
    ]);
});

app.get('/end', (req, res) => {
    res.send([
        "Должность 1",
        "Должность 2"
    ]);
});

// Другие методы API с аналогичной заглушкой

// Заглушки для различных типов активности
app.get('/positions', (req, res) => {
    // Возвращаем предварительное получение данных
    res.send('Предварительно получаем');
});

app.post('/positions', (req, res) => {
    // Игнорируем отправку данных
    res.send('Отправка');
});

app.get('/certifications', (req, res) => {
    // Возвращаем предварительное получение данных
    res.send('Предварительно получаем');
});

app.post('/certifications', (req, res) => {
    // Игнорируем отправку данных
    res.send('Отправка');
});

// Другие методы API для различных типов активности

// Обработка выговора
app.get('/rebuke', (req, res) => {
    // Возвращаем предварительное получение данных
    res.send('Предварительно получаем');
});

app.post('/rebuke', (req, res) => {
    // Обрабатываем отправку данных
    const { reason } = req.body;
    // Здесь можно выполнить необходимые действия с причиной выговора
    res.send('Отправка');
});

// Запуск сервера
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
