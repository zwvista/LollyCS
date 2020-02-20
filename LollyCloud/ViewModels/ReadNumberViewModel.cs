using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LollyShared
{
    public static class ReadNumber
    {
        public static string readInJapanese(int num)
        {
            var numbers1 = new [] { "ゼロ", "いち", "に", "さん", "よん", "ご", "ろく", "なな", "はち", "きゅう", "じゅう", "まん", "おく", "ちょう" };
            var numbers3 = new [] { "ひゃく", "にひゃく", "さんびゃく", "よんひゃく", "ごひゃく", "ろっぴゃく", "ななひゃく", "はっぴゃく", "きゅうひゃく" };
            var numbers4 = new [] { "せん", "にせん", "さんぜん", "よんせん", "ごせん", "ろっせん", "ななせん", "はっせん", "きゅうせん" };
            num = num % 1_0000_0000;
            string f(int n, string unit) {
                var (n4, n3, n2, n1) = (n / 1000, n % 1000 / 100, n % 100 / 10, n % 10);
                var s = n4 == 0 ? "" : numbers4[n4 - 1];
                s += n3 == 0 ? "" : numbers3[n3 - 1];
                s += n2 == 0 ? "" : n2 == 1 ? numbers1[10] : numbers1[n2] + numbers1[10];
                s += n1 == 0 ? "" : numbers1[n1];
                return s + unit;
            }
            if (num == 0)
                return numbers1[0];
            else
            {
                var n5 = num / 10000;
                var s1 = n5 == 0 ? "" : f(n5, numbers1[11]);
                var n1 = num % 10000;
                var s2 = n1 == 0 ? "" : f(n1, "");
                return string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) ? s1 + s2 : s1 + " " + s2;
            }
        }
        public static string readInNativeKorean(int num)
        {
            var numbers1 = new[] { "", "하나", "둘", "셋", "넷", "다섯", "여섯", "일곱", "여덟", "아홉" };
            var numbers2 = new [] { "", "열", "스물", "서른", "마흔", "쉰", "예순", "일흔", "여든", "아흔" };
            num = num % 100;
            var (n2, n1) = (num / 10, num % 10);
            var s1 = numbers2[n2];
            var s2 = numbers1[n1];
            return string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) ? s1 + s2 : s1 + " " + s2;
        }
        public static string readInSinoKorean(int num)
        {
            var numbers = new[] { "영", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구", "십", "백", "천", "만", "억", "조" };
            num = num % 1_0000_0000;
            string g(int n, int unit) =>
                n == 0 ? "" : n == 1 ? numbers[unit] : numbers[n] + numbers[unit];
            string f(int n, string unit) {
                var (n4, n3, n2, n1) = (n / 1000, n % 1000 / 100, n % 100 / 10, n % 10);
                var s = g(n: n4, unit: 12);
                s += g(n: n3, unit: 11);
                s += g(n: n2, unit: 10);
                s += n1 == 0 ? "" : numbers[n1];
                return s + unit;
            }
            if (num == 0)
                return numbers[0];
            else
            {
                var n5 = num / 10000;
                var s1 = n5 == 0 ? "" : n5 == 1 ? numbers[13] : f(n: n5, unit: numbers[13]);
                var n1 = num % 10000;
                var s2 = n1 == 0 ? "" : f(n: n1, unit: "");
                return string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) ? s1 + s2 : s1 + " " + s2;
            }
        }
    }
    public class ReadNumberViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        int _Number;
        public int Number
        {
            get => _Number;
            set => this.RaiseAndSetIfChanged(ref _Number, value);
        }
        string _Text;
        public string Text
        {
            get => _Text;
            set => this.RaiseAndSetIfChanged(ref _Text, value);
        }
        public ReadNumberViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
        }
    }
}
