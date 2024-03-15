import { useEffect, useState } from "react";
import ActivityComponentsProps from "./ActivityComponentsProps";

const ChangeSelaryActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            reason: string,
            value: number,
        }
    >(
        {
            reason: "",
            value: 0,
        }
    );

    const [reason, setReason] = useState<string>("")
    const [value, setValue] = useState<number>(0)

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
                <td>Причина: </td>
                <td>
                <textarea 
                    rows={3}
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