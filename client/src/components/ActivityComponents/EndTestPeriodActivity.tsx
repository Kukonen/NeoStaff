import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

interface EndTestPeriodActivityProps extends ActivityComponentsProps {
    serviceNumber: string;
    date: string;
}

const EndTestPeriodActivity = ({setData, serviceNumber, date}: EndTestPeriodActivityProps) => {
    const [currentData, setCurrentData] = useState<
        {
            position: string, 
            report: string,
            role: 'hired' | 'fired' | 'extended' | ''
        }
    >(
        {
            position: "", 
            report: "",
            role: "",
        }
    );
    
    const [positions, setPositions] = useState<string[]>([])

    const [report, setReport] = useState<string>("")

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getPositions(serviceNumber, date).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
    }, [])

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getPositions(serviceNumber, date).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
        
    }, [serviceNumber])

    const selectPosotion = (pos: string) => {
        let newData = currentData;
        currentData.position = pos;

        setCurrentData(newData);

        setData(newData);
    }

    const changeReport = (newReport: string) => {
        setReport(newReport);

        let newData = currentData;
        currentData.report = newReport;

        setCurrentData(newData);

        setData(newData);
    }

    const changeRole = (newRole: 'принят в штат' | 'уволен' | 'увеличен испытательный срок') => {

        let newData = currentData;

        if (newRole === 'принят в штат') {
            currentData.role = 'hired';
        }

        if (newRole === 'уволен') {
            currentData.role = 'fired';
        }

        if (newRole === 'увеличен испытательный срок') {
            currentData.role = 'extended';
        }

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td>Позиция: </td>
                <td>
                    <Select 
                        options={positions}
                        setOption={pos => selectPosotion(pos)}
                    /> 
                </td>
            </tr>

            <tr>
                <td>Отзыв руководителя: </td>
                <td>
                    <input 
                        value={report}
                        onChange={e => changeReport(e.target.value)}
                    />
                </td>
            </tr>
            
            <tr>
                <td>Роль: </td>
                <td>
                    <Select 
                        options={['принят в штат', 'уволен', 'увеличен испытательный срок']}
                        setOption={r => changeRole(r as 'принят в штат' | 'уволен' | 'увеличен испытательный срок')}
                    /> 
                </td>
            </tr>
        </>
    )
}

export default EndTestPeriodActivity;