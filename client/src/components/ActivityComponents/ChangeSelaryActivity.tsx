import { useEffect, useState } from "react";
import Select from "../Select/Select";
import ActivityService from "../../service/ActivityService";
import ActivityComponentsProps from "./ActivityComponentsProps";

interface ChangeSelaryActivityProps extends ActivityComponentsProps {
    serviceNumber: string
}

const ChangeSelaryActivity = ({setData, serviceNumber}: ChangeSelaryActivityProps) => {
    const [currentData, setCurrentData] = useState<
        {
            position: string, 
            reason: string,
            value: number,
        }
    >(
        {
            position: "", 
            reason: "",
            value: 0,
        }
    );
    
    const [positions, setPositions] = useState<string[]>([])

    const [reason, setReason] = useState<string>("")
    const [value, setValue] = useState<number>(0)

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getEndTestPeriod(serviceNumber).then(positionFromServer => {
                setPositions(positionFromServer as string[]);
            })
        }
    }, [])

    useEffect(() => {
        if (serviceNumber !== "") {
            ActivityService.getEndTestPeriod(serviceNumber).then(positionFromServer => {
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

    const changeReason = (newReason: string) => {
        setReason(newReason);

        let newData = currentData;
        currentData.reason = newReason;

        setCurrentData(newData);

        setData(newData);
    }

    const changeValue = (newValue: number) => {
        setValue(newValue);

        let newData = currentData;
        currentData.value = newValue;
        

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
                <td>Причина: </td>
                <td>
                    <input 
                        value={reason}
                        onChange={e => changeReason(e.target.value)}
                    />
                </td>
            </tr>
            
            <tr>
                <td>Размер: </td>
                <td>
                    <input 
                        type="number"
                        value={value}
                        onChange={e => changeValue(Number(e.target.value))}
                    />
                </td>
            </tr>
        </>
    )
}

export default ChangeSelaryActivity;