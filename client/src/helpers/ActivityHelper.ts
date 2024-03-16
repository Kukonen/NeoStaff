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

export {converTypeToFormatType}