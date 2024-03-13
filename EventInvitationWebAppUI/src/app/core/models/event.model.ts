import { Invitations } from "./invitations"
import { User } from "./user.model"

export interface Events{
    id:string
    name:string
    startDate:Date
    endDate:Date
   creatorId:string
   creator:User
  invitation : Invitations
}