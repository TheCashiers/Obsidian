import Axios from 'axios';



export var getAxios = (token: string) => {
    return Axios.create({
        headers: { 'Authorization': `Bearer ${token}` }
    });
}