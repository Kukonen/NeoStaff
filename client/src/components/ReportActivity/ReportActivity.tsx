import { useEffect, useMemo, useState } from "react";
import { converTypeToFormatType } from "../../helpers/ActivityHelper";

export type TypeActivitiesFields = [
    string[]
] | [];

interface ReportActivityProps {
    typeActivitiesFields: TypeActivitiesFields
}

const ReportActivity = ({typeActivitiesFields} : ReportActivityProps) => {

    const formatterActivities = typeActivitiesFields.filter(field => field[0] !== 'type')

    const typeActivities = typeActivitiesFields.find(field => field[0] == 'type')

    return (
    <table className="table__action">
        <thead>
            <tr>
                <td colSpan={2} className="table__action__header">
                    {typeActivities ? converTypeToFormatType(typeActivities[1]) : undefined}
                    <hr />
                </td>
            </tr>
        </thead>
        <tbody className="table__action__block">
                {
                    formatterActivities.map(field => {
                        return (
                            <tr>
                                <td>
                                    {field[0]}
                                </td>
                                <td>
                                    <input 
                                        type="text" 
                                        value={field[1]}
                                        disabled
                                    />
                                </td>
                            </tr>
                        )
                    })
                }
        </tbody>
    </table>
    )
}

export default ReportActivity;