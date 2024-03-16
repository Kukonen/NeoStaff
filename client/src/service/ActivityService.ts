import server from "./server";

class ActivityService {

    static async getPositions(serviceNumber: string, date: string) {
        return new Promise((resolve, reject) => {
            server('employees/date', 'GET', [
                {key: 'serviceNumber', value: serviceNumber},
                {key: 'date', value: date}
            ]).then(response => {
                resolve(response);
            }).catch(err => reject(err));
        })
    }

    static async getAllPositions() {
        return new Promise((resolve, reject) => {
            server('positions').then(response => {
                resolve(response);
            }).catch(err => reject(err));
        })
    }

    static async getCertification() {
        return new Promise((resolve, reject) => {
            server('certification').then(response => {
                resolve(response);
            }).catch(err => reject(err));
        })
    }

    static async postData(url: string, data: object) {
        return new Promise((resolve, reject) => {
            server(url, 'POST', undefined, data).then(response => {
                resolve(response);
            }).catch(err => reject(err));
        })
    }
}

export default ActivityService;