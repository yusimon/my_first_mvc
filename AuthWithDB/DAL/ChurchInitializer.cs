using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthWithDB.DAL
{
    public class ChurchInitializer : 
        System.Data.Entity.DropCreateDatabaseIfModelChanges<ChurchContext>
    {

    }
}