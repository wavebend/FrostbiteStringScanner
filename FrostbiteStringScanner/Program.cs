namespace FrostbiteStringScanner
{
    class Program
    {
        // global HashSet accumulating all found strings
        public static HashSet<string> foundStrings = new HashSet<string>();

        static void Main(string[] args)
        {
            FrostbiteStringScannerParams scannerParams = new FrostbiteStringScannerParams();
            List<string> sectionsToScan = new List<string>();
            string executableFilePath = string.Empty;
            string outputPath = string.Empty;

            // Set game to scan here
            ProfileVersion profileVersion = ProfileVersion.DragonAgeTheVeilguard;

            if (profileVersion == ProfileVersion.DragonAgeTheVeilguard)
            {
                executableFilePath = @"H:\SteamLibrary\steamapps\common\Dragon-Age-The-Veilguard\Dragon Age The Veilguard.exe";
                outputPath = @"G:\DAVG_strings.txt";
                sectionsToScan = new List<string> { ".rdata" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    EscapeSpecialCharacters = true,
                    ScanForUtf16Le = true,
                    StartOnString = "Morrison",
                    StopOnString = @"C:\DA_cert\TnT\Local\Bin\Morrison\Win64-steam\retail\Morrison.Main_Win64_retail.pdb",
                };
            }
            else if (profileVersion == ProfileVersion.Anthem)
            {
                executableFilePath = @"G:\EAPlay\Anthem\Anthem.exe";
                outputPath = @"G:\Anthem_strings.txt";
                sectionsToScan = new List<string> { ".arch" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    EscapeSpecialCharacters = true,
                    ScanForUtf16Le = true,
                    StartOnString = "Dylan",
                    StopOnString = @"C:\DYL\TnT\Local\Bin\Dylan\Win64\retail\Dylan.Main_Win64_retail.pdb"
                };
            }
            else if (profileVersion == ProfileVersion.NeedForSpeedUnbound)
            {
                executableFilePath = @"G:\EAPlay\Need for Speed Unbound\NeedForSpeedUnbound.exe";
                outputPath = @"G:\NFSUnbound_strings.txt";
                sectionsToScan = new List<string> { ".00cfg" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    EscapeSpecialCharacters = true,
                    ScanForUtf16Le = true,
                    StartOnString = "Excalibur",
                    StopOnString = @"D:\dev\TnT\Local\Bin\Nfs22\Win64\retail\Nfs22.Main_Win64_retail.pdb"
                };
            }
            else if (profileVersion == ProfileVersion.DragonAgeInquisition)
            {
                executableFilePath = @"G:\EAPlay\Dragon Age Inquisition\DragonAgeInquisition.exe";
                outputPath = @"G:\DAI_strings.txt";
                sectionsToScan = new List<string> { ".data1" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    EscapeSpecialCharacters = true,
                    ScanForUtf16Le = true,
                    StartOnString = "Dragon Age: Inquisition",
                    StopOnString = @"C:\monkey\bwmonkey-da3\tnt\local_win64_retail\Bin\DA3.Main_Win64_retail.pdb"
                };
            }

            FrostbiteStringScanner scanner = new FrostbiteStringScanner(executableFilePath, scannerParams);
            foreach (string sectionName in sectionsToScan)
            {
                scanner.ScanSection(sectionName, foundStrings);
            }

            File.WriteAllLines(outputPath, foundStrings);
            Console.WriteLine($"Found {foundStrings.Count} total unique strings across sections:");
            Console.WriteLine($"Output: {outputPath}");
        }

        // For demo purpose
        public enum ProfileVersion
        {
            NeedForSpeedRivals = 20131115,
            Battlefield4 = 20141117,
            DragonAgeInquisition = 20141118,
            NeedForSpeed = 20151103,
            StarWarsBattlefront = 20151117,
            PlantsVsZombiesGardenWarfare = 20140225,
            PlantsVsZombiesGardenWarfare2 = 20150223,
            MirrorsEdgeCatalyst = 20160607,
            Fifa17 = 20160927,
            Battlefield1 = 20161021,
            MassEffectAndromeda = 20170321,
            Fifa18 = 20170929,
            NeedForSpeedPayback = 20171110,
            StarWarsBattlefrontII = 20171117,
            Madden19 = 20180807,
            Fifa19 = 20180914,
            Battlefield5 = 20180628,
            NeedForSpeedEdge = 20171210,
            Anthem = 20181207,
            Madden20 = 20190729,
            PlantsVsZombiesBattleforNeighborville = 20190905,
            Fifa20 = 20190911,
            NeedForSpeedHeat = 20191101,
            StarWarsSquadrons = 20201001,
            Madden21 = 20200828,
            Fifa21 = 20201009,
            Madden22 = 20210820,
            Fifa22 = 20210927,
            Battlefield2042 = 20211119,
            Madden23 = 20220819,
            Fifa23 = 20220930,
            NeedForSpeedUnbound = 20221129,
            DeadSpace = 20230127,
            DragonAgeTheVeilguard = 20241031
        }
    }
}
