﻿using MargamParkArchivesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargamParkArchivesData
{
    public interface IArtefactReader
    {
        Artefact[] GetRandomArtefacts(int numArtefacts = 3);
    }
}
