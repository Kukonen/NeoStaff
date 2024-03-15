import server from "./server";

class ReportService {
    static async getActivities(serviceNumber: string) {
        return new Promise((resolve, reject) => {
            server('activities', 'GET', [{key: 'serviceNumber', value: serviceNumber}]).then((response) => {
                resolve(response);
            }).catch( e => reject(e))
        })
    }

    static async getGraphics(serviceNumber: string) {
        return new Promise((resolve, reject) => {
            server('graphics', 'GET', [{key: 'serviceNumber', value: serviceNumber}]).then((response) => {
                resolve(response);
            }).catch( e => reject(e))
        })
    }

    static async getPositions() {
        return new Promise((resolve, reject) => {
            server('positions', 'GET').then((response) => {
                resolve(response);
            }).catch( e => reject(e))
        })
    }
}

export default ReportService;