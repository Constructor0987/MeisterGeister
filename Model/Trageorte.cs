using System;

namespace MeisterGeister.Model
{
    public static class Trageorte
    {
        public static Guid Kopf { get; } = Guid.Parse("00000000-0000-0000-001a-000000000001");        
        public static Guid LinkerArm { get; } = Guid.Parse("00000000-0000-0000-001a-000000000002");
        public static Guid RechterArm { get; } = Guid.Parse("00000000-0000-0000-001a-000000000003");
        public static Guid RechteHand { get; } = Guid.Parse("00000000-0000-0000-001a-000000000004");
        public static Guid LinkeHand { get; } = Guid.Parse("00000000-0000-0000-001a-000000000005");
        public static Guid Rücken { get; } = Guid.Parse("00000000-0000-0000-001a-000000000006");
        public static Guid Oberkörper { get; } = Guid.Parse("00000000-0000-0000-001a-000000000007");
        public static Guid LinkesBein { get; } = Guid.Parse("00000000-0000-0000-001a-000000000008");
        public static Guid RechtesBein { get; } = Guid.Parse("00000000-0000-0000-001a-000000000009");
        public static Guid Schwanz { get; } = Guid.Parse("00000000-0000-0000-001a-000000000010");
        public static Guid Rucksack { get; } = Guid.Parse("00000000-0000-0000-001a-000000000011");
        public static Guid Tragetier { get; } = Guid.Parse("00000000-0000-0000-001a-000000000012");
    }
}
