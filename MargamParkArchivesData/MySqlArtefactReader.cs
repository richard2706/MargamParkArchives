using MargamParkArchivesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MargamParkArchivesData.DatabaseConstants;

namespace MargamParkArchivesData
{
    public class MySqlArtefactReader : IArtefactReader
    {
        private readonly IArtefactDataAccess _dataAccess;

        private const string _getRandomArtefactsQuery = "select * from {0} order by rand() limit {1};";

        public MySqlArtefactReader(IArtefactDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Artefact[] GetRandomArtefacts(int numArtefacts = 3)
        {
            string query = string.Format(_getRandomArtefactsQuery, ArtefactDetailsViewName, numArtefacts);
            return _dataAccess.GetArtefactList(query);
        }
    }
}
