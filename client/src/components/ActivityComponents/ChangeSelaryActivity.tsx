import { useEffect, useState } from "react";
import ActivityComponentsProps from "./ActivityComponentsProps";

const ChangeSelaryActivity = ({setData}: ActivityComponentsProps) => {
    const [currentData, setCurrentData] = useState<
        {
            reason: string,
        }
    >(
        {
            reason: ""
        }
    );

    const [reason, setReason] = useState<string>("")

    const changeReason = (newReason: string) => {
        setReason(newReason);

        let newData = currentData;
        currentData.reason = newReason;

        setCurrentData(newData);

        setData(newData);
    }

    return (
        <>
            <tr>
                <td className="td_top-diection">Причина: </td>
                <td>
                <textarea 
                    rows={3}
                    value={reason}
                    onChange={e => changeReason(e.target.value)}
                />
                </td>
            </tr>
        </>
    )
}

export default ChangeSelaryActivity;