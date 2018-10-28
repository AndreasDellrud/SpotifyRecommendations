using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyRekommendations.Entities.ResponseObjects
{
    public class CategoryRootObject
    {
        public ResponseObject<SpotifyCategory> Categories { get; set; }
    }
}
