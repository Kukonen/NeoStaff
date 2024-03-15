
db = db.getSiblingDB('StaffDB');


db.createCollection('position');
db.createCollection('employee');
db.createCollection('activity');



if (db.position.countDocuments() === 0) {
    db.position.insertMany([
        {
            "title": "Backend C# developer",
            "requirementScores": 10000
        },
        {
            "title": "Jesus",
            "requirementScores": 100000000
        }
    ]);
}

print(db.position.find())

if (db.activity.countDocuments() === 0) {
    db.activity.insertMany([
        {
            "date": new Date("2023-12-31"),
            "note": "Хорошо поработал",
            "mark": 100,
            "activityInfo": {
                "position": "Backend C# developer"
            },
            "type": "end"
        },
        {
            "date": new Date("2023-12-31"),
            "note": "Отличная работа, сын Мой!",
            "mark": 993,
            "activityInfo": {
                "position": "Jesus"
            },
            "type": "end"
        }
    ]);
}

print(db.activity.find())

if (db.employee.countDocuments() === 0) {
    db.employee.insertMany([
        {
            "name": "Павел",
            "surname": "Биглер",
            "middlename": "Павлович",
            "scores": 2086,
            "serviceNumber": "1111-2222-3333",
            "salary": 50000,
            "positions": [],
            "activities": [
                db.activity.findOne(
                    {
                        "note": "Хорошо поработал",
                        "mark": 100,
                        "type": "end"
                    }
                )._id,
                db.activity.findOne(
                    {
                        "note": "Отличная работа, сын Мой!",
                        "mark": 993,
                        "type": "end"
                    }
                )._id
            ]
        },
        {
            "name": "Евгений",
            "surname": "Куконен",
            "middlename": "Игоревич",
            "scores": 0,
            "serviceNumber": "2222-3333-4444",
            "salary": 70000,
            "positions": [],
            "activities": []
        }
    ]);
}


print(db.employee.find())