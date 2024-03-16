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

app.get('/activities', (req, res) => {
    const { serviceNumber } = req.query;

    if (serviceNumber !== "111") {
        return res.status(400).send('Не указан тип активности в query параметрах');
    }

    // Возвращаем предварительное получение данных
    res.send([
        {
            "_id": "65f426e4fc12ea27eeb8e2fd",
            "date": "2023-12-31",
            "note": "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
            "mark": 100,
            "activityInfo": {
                "position": "Backend c# developer"
            },
            "type": "start"
        },
        {
            "_id": "65f43ad1417dc0a26b6eeca4",
            "date": "2023-12-31",
            "note": "Отличная работа, сын Мой!",
            "mark": 993,
            "activityInfo": {
                "position": "Jesus"
            },
            "type": "end"
        }
    ]);
});

app.get('/graphics', (req, res) => {
    const { serviceNumber } = req.query;

    if (serviceNumber !== "111") {
        return res.status(400).send('Не указан тип активности в query параметрах');
    }

    // Возвращаем предварительное получение данных
    res.send([
        {
            "date": '2014-06-02',
            "salary": 10000,
            positions: [
                "Программист"
            ],
            scores: 500
        },
        {
            "date": '2014-08-02',
            "salary": 10000,
            positions: [
                "Программист"
            ],
            scores: 600
        },
        {
            "date": '2014-18-02',
            "salary": 10000,
            positions: [
                "Программист"
            ],
            scores: 650
        },
        {
            "date": '2014-20-02',
            "salary": 15000,
            positions: [
                "Программист"
            ],
            scores: 700
        },
        {
            "date": '2014-21-02',
            "salary": 25000,
            positions: [
                "Программист"
            ],
            scores: 700
        },
        {
            "date": '2014-24-02',
            "salary": 25000,
            positions: [
                "Программист"
            ],
            scores: 600
        },
        {
            "date": '2014-20-03',
            "salary": 20000,
            positions: [
                "Программист",
                "Начальник туалета"
            ],
            scores: 600
        },
        {
            "date": '2014-24-03',
            "salary": 21000,
            positions: [
                "Программист",
                "Начальник туалета"
            ],
            scores: 650
        },
        {
            "date": '2015-24-03',
            "salary": 20000,
            positions: [
                "Программист"
            ],
            scores: 650
        },
    ]);
});

app.get('/positions', (req, res) => {
    res.send([
        {
            title: "Программист",
            requirementScores: 1000
        },
        {
            title: "Начальник туалета",
            requirementScores: 4000
        }
    ]);
});


// Запуск сервера
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
