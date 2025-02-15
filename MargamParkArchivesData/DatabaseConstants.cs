namespace MargamParkArchivesData
{
    internal static class DatabaseConstants
    {
        // Artefact table
        internal const string ArtefactTableName = "artefact";
        internal const string ArtefactIdentiferGroupId = "identifier_group_id";
        internal const string ArtefactIdentifierNumber = "identifier_number";
        internal const string ArtefactIdentifierKey = "identifier_key";
        internal const string ArtefactFilePath = "file_path";
        internal const string ArtefactDateCreated = "date_created";
        internal const string ArtefactDateModified = "date_modified";
        internal const string ArtefactParentId = "parent_id";
        internal const string ArtefactNotes = "notes";
        internal const string ArtefactTitleEn = "title_en";
        internal const string ArtefactTitleCy = "title_cy";
        internal const string ArtefactDescriptionEn = "Description_en";
        internal const string ArtefactDescriptionCy = "Description_cy";
        internal const string ArtefactTagsCy = "tags_cy";
        internal const string ArtefactCultureTagEn = "culture_tag_en";
        internal const string ArtefactLocationCoverage = "location_coverage";
        internal const string ArtefactRightType1 = "right_type_1";
        internal const string ArtefactRightHolder1En = "right_holder_1_en";
        internal const string ArtefactRightHolder1Cy = "right_holder_1_cy";
        internal const string ArtefactVisualArtefact = "visual_artefact";

        internal const string CategoryId = "category_id";
        internal const string PeriodId = "period_id";
        internal const string CreatorId = "creator_id";
        internal const string GeneralLocationId = "general_location_id";
        internal const string SpecificLocationId = "specific_location_id";

        // ArtefactDetails view (in addition to Artefact table)
        internal const string ArtefactDetailsViewName = "artefact_details";
        internal const string ArtefactDetailsIdentifierGroupName = "identifier_group_name";
        internal const string ArtefactDetailsCategoryName = "category_name";
        internal const string ArtefactDetailsCreatorName = "creator_name";
        internal const string ArtefactDetailsGeneralLocationName = "general_location_name";
        internal const string ArtefactDetailsSpecificLocationSummary = "specific_location_summary";
        internal const string ArtefactDetailsPeriodDates = "period_dates";
    }
}
