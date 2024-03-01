import axios from "axios";
import { Ticket } from "../dataModels/Ticket";
import { devEnvironment } from "../environments/devEnvironments";
import { ListaTickets } from "../dataModels/ListaTickets";

export interface CreateTicketDTO {
    titulo: string,
    descricao: string,
    prioridade: number,
    produtoId: number
}

export interface UpdateTicketDTO {
    titulo: string,
    descricao: string,
    prioridade: number,
    produtoId: number
}

const TICKETS_URL = devEnvironment.BACKEND_URL + "/Tickets";

export class TicketService {

    async getTickets(abortController?: AbortController) :  Promise<ListaTickets>{
        let res = await axios.request<ListaTickets>({
            method: 'GET',
            url: TICKETS_URL,
            signal: abortController?.signal
        })
        
        return res.data;
    
    }

    async getTicketById(id: number, abortController?: AbortController) :  Promise<Ticket>{
        let res = await axios.request<Ticket>({
            method: 'GET',
            url: TICKETS_URL+ `/${id}`,
            signal: abortController?.signal
        })
        
        return res.data;
    }
    
    async createTicket(ticket: CreateTicketDTO){
        
        let res = await axios.request<Ticket>({
            method: 'POST',
            url: TICKETS_URL,
            data: ticket
        })
        return res;
    }

    async updateTicket(id: number, newTicket : UpdateTicketDTO){

        let res = await axios.request<Ticket>({
            method: 'PUT',
            url: TICKETS_URL + `/${id}`,
            data: newTicket
        })

        return res;
    }
}