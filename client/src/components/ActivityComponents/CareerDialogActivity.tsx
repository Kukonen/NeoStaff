import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

const CareerDialogActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            report: string,
            result: 'positive' | 'negative' | 'neural' | ''
        }
    >(
        {
            report: "", 
            result: "",
        }
    );
    const [report, setReport] = useState<string>("")

    const changeReport = (newReport: string) => {
        setReport(newReport);

        let newData = currentData;
        currentData.report = newReport;

        setCurrentData(newData);

        setData(newData);
    }

    const changeResult = (newResult: 'нейтральный' | 'позитивный' | 'негативный') => {

        let newData = currentData;

        if (newResult === 'нейтральный') {
            currentData.result = 'neural';
        }

        if (newResult === 'позитивный') {
            currentData.result = 'positive';
        }

        if (newResult === 'негативный') {
            currentData.result = 'negative';
        }

        setCurrentData(newData);

        setData(newData);
    }

    

    return (
        <>
            <tr>
                <td>Место соревнования: </td>
                <td>
                    <input 
                        value={report}
                        onChange={e => changeReport(e.target.value)}
                    />
                </td>
            </tr>
            
            <tr>
                <td>Рузультат диалога: </td>
                <td>
                    <Select 
                        options={['нейтральный', 'позитивный', 'негативный']}
                        setOption={r => changeResult(r as 'нейтральный' | 'позитивный' | 'негативный')}
                    /> 
                </td>
            </tr>
        </>
    )
}

export default CareerDialogActivity;