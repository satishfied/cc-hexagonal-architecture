using System;

namespace Recruiting.Domain.Infrastructure
{
   public class Entity
   {
       private Guid _id;

       public Entity(Guid id)
       {
           this._id = id;
       }

       public Guid Id
       {
           get
           {
               return this._id;
           }
       }
   }
}
