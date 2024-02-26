import axios from "axios";
import { Ticket } from "../dataModels/Ticket";
import { devEnvironment } from "../environments/devEnvironments";

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

    async getTickets() :  Promise<Ticket[]>{
        let res = await axios.request<Ticket[]>({
            method: 'GET',
            url: TICKETS_URL
        })
        
        //TODO error handling
        return res.data;
    
    }

    async getTicketById(id: number) :  Promise<Ticket>{
        let res = await axios.request<Ticket>({
            method: 'GET',
            url: TICKETS_URL+ `/${id}`
        })
        
        //TODO error handling
        return res.data;
    }
    
    async createTicket(ticket: CreateTicketDTO){
        
        let res = await axios.request<Ticket>({
            method: 'POST',
            url: TICKETS_URL,
            data: ticket
        })
        
        //TODO error handling
    }

    async updateTicket(id: number, newTicket : UpdateTicketDTO){

        let res = await axios.request<Ticket>({
            method: 'PUT',
            url: TICKETS_URL + `/${id}`,
            data: newTicket
        })

        //TODO error handling
    }
}