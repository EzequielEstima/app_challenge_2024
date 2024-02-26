import { Ticket } from "../dataModels/Ticket";

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

const tickets =[{
    id: 1,
    titulo: "Ticket 1",
    descricao: "Descrição do ticket 1",
    prioridade: 1,
    produtoId: 1
},
{
    id: 2,
    titulo: "Ticket 2",
    descricao: "Descrição do ticket 2",
    prioridade: 2,
    produtoId: 2
}
];
let id = 3;
export class TicketService {

    async getTickets() :  Promise<Ticket[]>{
        return tickets;
    }

    async getTicketById(id: number) :  Promise<Ticket>{
        const res =  tickets.find(ticket => ticket.id === id);
        
        if(!res) throw new Error("FML");
        return res;
    }
    
    async createTicket(ticket: CreateTicketDTO){
        
        let newTicket: Ticket = {
            id: id++,
            titulo: ticket.titulo,
            descricao: ticket.descricao,
            prioridade: ticket.prioridade,
            produtoId: ticket.produtoId
        }
        
        // 3
        // ticket.id = id++; //ticket.id=3, id=4
        // ticket.id = ++id; //ticket.id=4, id=4

        tickets.push(newTicket);
    }

    async updateTicket(id: number, newTicket : UpdateTicketDTO){
        const res =  tickets.find(ticket => ticket.id === id);
        
        if(!res) throw new Error("FML");
        
        res.titulo = newTicket.titulo
        res.descricao = newTicket.descricao
        res.prioridade = newTicket.prioridade
        res.produtoId = newTicket.produtoId
    }
}