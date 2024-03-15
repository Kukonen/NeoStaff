import server from "./server";

class ReportService {
    static async getActivities(serviceNumber: string) {
        return new Promise((resolve, reject) => {
            server('report', 'GET', [{key: 'serviceNumber', value: serviceNumber}]).then(response => {
                resolve(response);
            })
        })
    }
}

export default ReportService;