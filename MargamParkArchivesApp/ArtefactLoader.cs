using MargamParkArchivesData;
using MargamParkArchivesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchivesApp
{
    internal class ArtefactLoader
    {
        private IArtefactReader _artefactReader;

        public ArtefactLoader()
        {
            _artefactReader = new MySqlArtefactReader(new MySqlArtefactDataAccess());
        }

        public ArtefactLoader(IArtefactReader artefactReader)
        {
            _artefactReader = artefactReader;
        }

        internal Artefact[] GetRandomArtefactList()
        {
            return _artefactReader.GetRandomArtefacts();
        }
    }
}
