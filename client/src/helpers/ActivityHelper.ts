const converTypeToFormatType = (type: string) =>  {
    switch (type) {
        case "start":
            return "Вступление в должность";
        case "end":
            return "Окончание работы в должности";
        case "certification":
            return "Аттестация";
        case "learn":
            return "Обучение";
        case "competition":
            return "Соревнование";
        case "event":
            return "Мероприятие";
        case "endTestPeriod":
            return "Окончание испытательного срока";
        case "changeSalary":
            return "Изменение заработной платы";
        case "skills":
            return "Повышение квалификации";
        case "careerDialog":
            return "Карьерный диалог";
        case "rebuke":
            return "Нарушение/выговор";
        default:
            return "Некорректный тип события";
    }
}

const titlesConverter = (title: string) => {
    switch (title) {
        case "start":
            return "Вступление в должность";
        case "end":
            return "Окончание работы в должности";
        case "certification":
            return "Аттестация";
        case "learn":
            return "Обучение";
        case "competition":
            return "Соревнование";
        case "event":
            return "Мероприятие";
        case "endTestPeriod":
            return "Окончание испытательного срока";
        case "changeSalary":
            return "Изменение заработной платы";
        case "skills":
            return "Повышение квалификации";
        case "careerDialog":
            return "Карьерный диалог";
        case "rebuke":
            return "Нарушение/выговор";
        case "report":
            return "Отзыв";
        case "result":
            return "Результат";
        case "certification":
            return "Аттестация";
        case "reason":
            return "Причина";
        case "place":
            return "Место";
        case "theme":
            return "Тема";
        case "role":
            return "Роль";
        case "position":
            return "Должность";
        case "specialization":
            return "Направление";
        case "skill":
            return "Навык";
        case "document":
            return "Подтврерждающий документ";
        case "neural":
            return "Нейтральный";
        case "positive":
            return "Позитивный";
        case "negative":
            return "Негативный";
        case "participant":
            return "Участник";
        case "organizer":
            return "Организатор";
        case "jury":
            return "Член жюри";
        case "hired":
            return "Принят в штат";
        case "fired":
            return "Уволен";
        case "extended":
            return "Увеличен испытательный срок";
        case "speaker":
            return "Выступающий";
        case "note":
            return "Заметка";
        case "mark":
            return "Баллы";
        case "salary":
            return "Изменение зарплаты";
        default:
            return title;
    }
}

const dateConverter = (date: string) => {
    const dateFormatter = date.split("-");
    return dateFormatter[2] + '.' + dateFormatter[1] + '.' + dateFormatter[0];
}

export {converTypeToFormatType, titlesConverter, dateConverter}