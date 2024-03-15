import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const LearnActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            place: string, 
            result: string,
            specialization: string,
            document: string
        }
    >(
        {
            place: "", 
            result: "",
            specialization: "",
            document: ""
        }
    );
    const [place, setPlace] = useState<string>("")
    const [result, setResult] = useState<string>("")
    const [specialization, setSpecialization] = useState<string>("")
    const [document, setDocument] = useState<string>("")

    const changePlace = (newPlace: string) => {
        setPlace(newPlace);

        let newData = currentData;
        currentData.place = newPlace;

        setCurrentData(newData);

        setData(newData);
    }

    const changeResult = (newResult: string) => {
        setResult(newResult);

        let newData = currentData;
        currentData.result = newResult;

        setCurrentData(newData);

        setData(newData);
    }

    const changeSpecialization = (newSpecialization: string) => {
        setSpecialization(newSpecialization);

        let newData = currentData;
        currentData.specialization = newSpecialization;

        setCurrentData(newData);

        setData(newData);
    }

    const changeDocument = (newDocument: string) => {
        setDocument(newDocument);

        let newData = currentData;
        currentData.document = newDocument;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td>Направление обучения: </td>
                <td>
                    <input 
                        type="text"
                        value={specialization}
                        onChange={e => changeSpecialization(e.target.value)}
                    />
                </td>
            </tr>
            <tr>
                <td>Место обучения: </td>
                <td>
                    <input 
                        type="text"
                        value={place}
                        onChange={e => changePlace(e.target.value)}
                    />
                </td>
            </tr>
            <tr>
                <td>Результат обучения: </td>
                <td>
                    <input 
                        type="text"
                        value={result}
                        onChange={e => changeResult(e.target.value)}
                    />
                </td>
            </tr>
            <tr>
                <td>Подтверждающий документ: </td>
                <td>
                    <input 
                        type="text"
                        value={document}
                        onChange={e => changeDocument(e.target.value)}
                    />
                </td>
            </tr>
        </>
    )
}

export default LearnActivity;