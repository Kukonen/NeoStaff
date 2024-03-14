import server from "./server";

class ActivityService {
    static async getStart() {
        return new Promise((resolve, reject) => {
            server('start').then(response => {
                resolve(response);
            })
        })
    }

    static async getEnd(serviceNumber: string) {
        return new Promise((resolve, reject) => {
            server('end', 'GET', [{key: 'serviceNumber', value: serviceNumber}]).then(response => {
                resolve(response);
            })
        })
    }

    static async getCertification() {
        return new Promise((resolve, reject) => {
            server('certification').then(response => {
                resolve(response);
            })
        })
    }

    static async postData(url: string, data: object) {
        return new Promise((resolve, reject) => {
            server(url, 'POST', undefined, data).then(response => {
                resolve(response);
            })
        })
    }
}

export default ActivityService;