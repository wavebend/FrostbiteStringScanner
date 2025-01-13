using static FrostbiteStringScanner.ProfilesLibrary.ProfileVersion;

namespace FrostbiteStringScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            FrostbiteStringScannerParams scannerParams = new FrostbiteStringScannerParams();
            List<string> sectionsToScan = new List<string>();
            string executableFilePath = string.Empty;
            string outputPath = string.Empty;

            // Set game to scan here
            ProfilesLibrary.version = DragonAgeInquisition;

            if (ProfilesLibrary.version == DragonAgeTheVeilguard)
            {
                executableFilePath = @"H:\SteamLibrary\steamapps\common\Dragon-Age-The-Veilguard\Dragon Age The Veilguard.exe";
                outputPath = @"G:\DAVG_strings.txt";
                sectionsToScan = new List<string> { ".rdata" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Morrison",
                    //StopOnString = @"C:\DA_cert\TnT\Local\Bin\Morrison\Win64-steam\retail\Morrison.Main_Win64_retail.pdb", // This includes slightly more junk
                    StopOnString = "ReplayToc",
                };
            }
            else if (ProfilesLibrary.version == MassEffectAndromeda)
            {
                executableFilePath = @"G:\EAPlay\Mass Effect Andromeda\MassEffectAndromeda.exe";
                outputPath = @"G:\MEA_strings.txt";
                sectionsToScan = new List<string> { ".rdata" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Future",
                    // StopOnString = @"C:\MEA\rc\TnT\Local\Bin\Contact\Win64\retail\Contact.Main_Win64_retail.pdb", // This includes slightly more junk
                    StopOnString = "Registry"
                };
            }
            else if (ProfilesLibrary.version == Anthem)
            {
                executableFilePath = @"G:\EAPlay\Anthem\Anthem.exe";
                outputPath = @"G:\Anthem_strings.txt";
                sectionsToScan = new List<string> { ".arch" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Dylan",
                    // StopOnString = @"C:\DYL\TnT\Local\Bin\Dylan\Win64\retail\Dylan.Main_Win64_retail.pdb", // This includes slightly more junk
                    StopOnString = "$Element"
                };
            }
            else if (ProfilesLibrary.version == DragonAgeInquisition)
            {
                executableFilePath = @"G:\EAPlay\Dragon Age Inquisition\DragonAgeInquisition.exe";
                outputPath = @"G:\DAI_strings.txt";
                sectionsToScan = new List<string> { ".data1" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Dragon Age: Inquisition",
                    // StopOnString = @"C:\monkey\bwmonkey-da3\tnt\local_win64_retail\Bin\DA3.Main_Win64_retail.pdb" // This includes slightly more junk
                    StopOnString = "Stereo"
                };
            }
            else if (ProfilesLibrary.version == NeedForSpeedUnbound)
            {
                executableFilePath = @"G:\EAPlay\Need for Speed Unbound\NeedForSpeedUnbound.exe";
                outputPath = @"G:\NFSUnbound_strings.txt";
                sectionsToScan = new List<string> { ".00cfg" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Excalibur",
                    StopOnString = @"D:\dev\TnT\Local\Bin\Nfs22\Win64\retail\Nfs22.Main_Win64_retail.pdb"
                };
            }
            else if (ProfilesLibrary.version == StarWarsBattlefrontII)
            {
                executableFilePath = @"G:\EAPlay\STAR WARS Battlefront II\starwarsbattlefrontii.exe";
                outputPath = @"G:\SWBF2_strings.txt";
                sectionsToScan = new List<string> { ".idata" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Walrus",
                    StopOnString = @"D:\dev\TnT\Local\Bin\WS\Win64\retail\WS.Main_Win64_retail.pdb"
                };
            }
            else if (ProfilesLibrary.version == Battlefield1)
            {
                executableFilePath = @"G:\EAPlay\bf1.exe";
                outputPath = @"G:\EAPlay\bf1_strings.txt";
                sectionsToScan = new List<string> { ".data2" };
                scannerParams = new FrostbiteStringScannerParams()
                {
                    StartOnString = "Tunguska.Tunguska.{}",
                    StopOnString = @"E:\dev\tun\TnT\Local\Bin\Tunguska.Main_Win64_retail.pdb"
                };
            }

            FrostbiteStringScanner scanner = new FrostbiteStringScanner(executableFilePath, scannerParams);

            // Scan sections
            HashSet<string> foundStrings = new HashSet<string>();
            foreach (string sectionName in sectionsToScan)
            {
                scanner.ScanSection(sectionName, foundStrings);
            }

            Console.WriteLine($"Found {foundStrings.Count} total unique strings across sections:");

            // Run the generic filtering string pipeline (not necessary)
            StringPipeline pipeline = ExeStringPipeline.GetExeStringPipeline();
            var (finalStrings, generatedStrings) = pipeline.Run(foundStrings);

            Console.WriteLine($"After applying generic filtering pipeline, {finalStrings.Count} strings remain");

            // Write the strings to file. Change to foundStrings if you want the raw/unfiltered strings from the string scanner
            File.WriteAllLines(outputPath, finalStrings);
            Console.WriteLine($"Output has been written to: {outputPath}");
            Console.WriteLine($"Press any key to exit");
            Console.ReadKey();
        }
    }
}
