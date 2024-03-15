
export type TypeActivitiesFields = [
    string[]
] | [];

interface ReportActivityProps {
    typeActivitiesFields: TypeActivitiesFields
}

const ReportActivity = ({typeActivitiesFields} : ReportActivityProps) => {
    return (
        <table>
            <caption>
                {"type"}
            </caption>
            <tbody>
                {
                    typeActivitiesFields.map(field => {
                        return (
                            <tr>
                                <td>
                                    {field[0]}
                                </td>
                                <td>
                                    {field[1]}
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