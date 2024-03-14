import { Events } from "./event.model"
import { User } from "./user.model"

export interface Invitations{
   eventId:string
   userId:string
   respondingUserName:string
   status:string
}