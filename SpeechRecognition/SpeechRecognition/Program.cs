using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeechRecognition
{
    class Program
    {
        #region Private Members

        static string projectName = "sanskrit";

        static List<string> proto = new List<string>()
        {
            "~o",
            "<STREAMINFO> 1 25",
            "<VECSIZE> 25<NULLD><MFCC_D_N_Z_0><DIAGC>",
            "~h \"proto\"",
            "<BEGINHMM>",
            "<NUMSTATES> 5",
            "<STATE> 2",
            "<MEAN> 25",
            " 9.530112e-006 -2.138562e-007 -7.917783e-006 2.497387e-006 -5.392363e-007 -1.253323e-006 -2.348470e-006 -2.648128e-006 -3.818038e-006 3.742653e-006 7.508473e-007 9.400568e-006 -6.669673e-006 1.109301e-005 4.060623e-006 4.368586e-006 1.645758e-006 6.497407e-006 8.898613e-006 5.746782e-006 3.979650e-008 -1.872893e-006 -4.361145e-006 5.683224e-008 4.266507e-005",
            "<VARIANCE> 25",
            " 1.172581e+001 1.590255e+001 2.678108e+001 3.211390e+001 3.510873e+001 5.320559e+001 2.828191e+001 4.151841e+001 2.652094e+001 2.928319e+001 2.776626e+001 3.002477e+001 2.100420e-001 4.844300e-001 7.319461e-001 8.984082e-001 1.207790e+000 1.492907e+000 1.559892e+000 1.820638e+000 1.665788e+000 1.736126e+000 1.588265e+000 1.520801e+000 1.671014e-001",
            "<GCONST> 8.499754e+001",
            "<STATE> 3",
            "<MEAN> 25",
            " 9.530112e-006 -2.138562e-007 -7.917783e-006 2.497387e-006 -5.392363e-007 -1.253323e-006 -2.348470e-006 -2.648128e-006 -3.818038e-006 3.742653e-006 7.508473e-007 9.400568e-006 -6.669673e-006 1.109301e-005 4.060623e-006 4.368586e-006 1.645758e-006 6.497407e-006 8.898613e-006 5.746782e-006 3.979650e-008 -1.872893e-006 -4.361145e-006 5.683224e-008 4.266507e-005",
            "<VARIANCE> 25",
            " 1.172581e+001 1.590255e+001 2.678108e+001 3.211390e+001 3.510873e+001 5.320559e+001 2.828191e+001 4.151841e+001 2.652094e+001 2.928319e+001 2.776626e+001 3.002477e+001 2.100420e-001 4.844300e-001 7.319461e-001 8.984082e-001 1.207790e+000 1.492907e+000 1.559892e+000 1.820638e+000 1.665788e+000 1.736126e+000 1.588265e+000 1.520801e+000 1.671014e-001",
            "<GCONST> 8.499754e+001",
            "<STATE> 4",
            "<MEAN> 25",
            " 9.530112e-006 -2.138562e-007 -7.917783e-006 2.497387e-006 -5.392363e-007 -1.253323e-006 -2.348470e-006 -2.648128e-006 -3.818038e-006 3.742653e-006 7.508473e-007 9.400568e-006 -6.669673e-006 1.109301e-005 4.060623e-006 4.368586e-006 1.645758e-006 6.497407e-006 8.898613e-006 5.746782e-006 3.979650e-008 -1.872893e-006 -4.361145e-006 5.683224e-008 4.266507e-005",
            "<VARIANCE> 25",
            " 1.172581e+001 1.590255e+001 2.678108e+001 3.211390e+001 3.510873e+001 5.320559e+001 2.828191e+001 4.151841e+001 2.652094e+001 2.928319e+001 2.776626e+001 3.002477e+001 2.100420e-001 4.844300e-001 7.319461e-001 8.984082e-001 1.207790e+000 1.492907e+000 1.559892e+000 1.820638e+000 1.665788e+000 1.736126e+000 1.588265e+000 1.520801e+000 1.671014e-001",
            "<GCONST> 8.499754e+001",
            "<TRANSP> 5",
            " 0.000000e+000 1.000000e+000 0.000000e+000 0.000000e+000 0.000000e+000",
            " 0.000000e+000 6.000000e-001 4.000000e-001 0.000000e+000 0.000000e+000",
            " 0.000000e+000 0.000000e+000 6.000000e-001 4.000000e-001 0.000000e+000",
            " 0.000000e+000 0.000000e+000 0.000000e+000 7.000000e-001 3.000000e-001",
            " 0.000000e+000 0.000000e+000 0.000000e+000 0.000000e+000 0.000000e+000",
            "<ENDHMM>"
        };

        static Dictionary<string, List<string>> keyValues = new Dictionary<string, List<string>>() {
            { "अ", new List<string>() { "[ɐ]", "[ə]", "[a]" } },
            { "आ", new List<string>() { "[aː]", "[ā]" } },
            { "इ", new List<string>() { "[i]" } },
            { "ई", new List<string>() { "[iː]", "[ī]" } },
            { "उ", new List<string>() { "[u]" } },
            { "ऊ", new List<string>() { "[uː]", "[ū]" } },
            { "ऋ", new List<string>() { "[r̰]", "[ɹ̩]", "[r̥]" } },
            { "ॠ", new List<string>() { "[r̰ː]", "[ɹ̩ː]", "[r̥̄]" } },
            { "ऌ", new List<string>() { "[l̩]", "[l̥]" } },
            { "ॡ", new List<string>() { "[l̩ː]", "[l̥̄]" } },
            { "ए", new List<string>() { "[eː]", "[e]" } },
            { "ऐ", new List<string>() { "[a͡ːɪ]", "[ai]" } },
            { "ओ", new List<string>() { "[oː]", "[o]" } },
            { "औ", new List<string>() { "[a͡ːʊ]", "[au]" } },
            { "ऽ", new List<string>() { "[ɐ]", "[ə]", "[a]" } },
            { "ा", new List<string>() { "[aː]", "[ā]" } },
            { "ि", new List<string>() { "[i]" } },
            { "ी", new List<string>() { "[iː]", "[ī]" } },
            { "ु", new List<string>() { "[u]" } },
            { "ू", new List<string>() { "[uː]", "[ū]" } },
            { "ृ", new List<string>() { "[r̰]", "[ɹ̩]", "[r̥]" } },
            { "ॄ", new List<string>() { "[r̰ː]", "[ɹ̩ː]", "[r̥̄]" } },
            { "ॢ", new List<string>() { "[l̩]", "[l̥]" } },
            { "ॣ", new List<string>() { "[l̩ː]", "[l̥̄]" } },
            { "े", new List<string>() { "[eː]", "[e]" } },
            { "ै", new List<string>() { "[a͡ːɪ]", "[ai]" } },
            { "ो", new List<string>() { "[oː]", "[o]" } },
            { "ौ", new List<string>() { "[a͡ːʊ]", "[au]" } },
            { "ं", new List<string>() { "[m̥]" } },
            { "ः", new List<string>() { "[h̥]", "[x]" } },
            { "क", new List<string>() { "[k]" } },
            { "ख", new List<string>() { "[kʰ]", "[kh]" } },
            { "ग", new List<string>() { "[ɡ]" } },
            { "घ", new List<string>() { "[ɡʱ]", "[ɡh]" } },
            { "ङ", new List<string>() { "[ŋ]" } },
            { "च", new List<string>() { "[c]", "[tʃ]" } },
            { "छ", new List<string>() { "[cʰ]", "[tʃʰ]" } },
            { "ज", new List<string>() { "[ɟ]", "[dʒ]" } },
            { "झ", new List<string>() { "[ɟʱ]", "[dʒʱ]" } },
            { "ञ", new List<string>() { "[ɲ]" } },
            { "ट", new List<string>() { "[ʈ]" } },
            { "ठ", new List<string>() { "[ʈʰ]" } },
            { "ड", new List<string>() { "[ɖ]" } },
            { "ढ", new List<string>() { "[ɖʱ]" } },
            { "ण", new List<string>() { "[ɳ]" } },
            { "त", new List<string>() { "[t̪]" } },
            { "थ", new List<string>() { "[t̪ʰ]" } },
            { "द", new List<string>() { "[d̪]" } },
            { "ध", new List<string>() { "[d̪ʱ]" } },
            { "न", new List<string>() { "[n]" } },
            { "प", new List<string>() { "[p]" } },
            { "फ", new List<string>() { "[pʰ]" } },
            { "ब", new List<string>() { "[b]" } },
            { "भ", new List<string>() { "[bʱ]" } },
            { "म", new List<string>() { "[m]" } },
            { "य", new List<string>() { "[j]" } },
            { "र", new List<string>() { "[ɾ]" } },
            { "ल", new List<string>() { "[l]" } },
            { "व", new List<string>() { "[ʋ]" } },
            { "श", new List<string>() { "[ʃ]" } },
            { "ष", new List<string>() { "[ʂ]" } },
            { "स", new List<string>() { "[s]" } },
            { "ह", new List<string>() { "[ɦ]" } },
            { "ॐ", new List<string>() { "[ɦ]" } },
            { "॑", new List<string>() { "[ˈ]" } },
            { "॒", new List<string>() { "[_]" } },
            { "॓", new List<string>() { "[ː]" } },
            { "्", new List<string>() { "[ɦ]" } },
            { "உ", new List<string>() { "[ɐ]", "[ə]", "[a]" } },
            { "।", new List<string>() { "[।]" } },
            { "॥", new List<string>() { "[॥]" } },
            { " ", new List<string>() { "[॥]" } },
            { "SENT-END", new List<string>() { "[sil]" } },
            { "SENT-START", new List<string>() { "[sil]" } }
        };

        static string dataPath = @"C:\Users\Jaffa\Desktop\CMUSphinx\voxforge\sanskrit\";

        static string path = dataPath + projectName + "\\";

        static List<KeyValuePair<int, int>> fromTo = new List<KeyValuePair<int, int>>()
        {
            new KeyValuePair<int, int>(2304, 2431),
            new KeyValuePair<int, int>(7376, 7417),
            new KeyValuePair<int, int>(43232, 43263),
            new KeyValuePair<int, int>(32, 32)
        };

        static List<string> TreeHed = new List<string>()
        {
            "RO 100 \"stats\"",
            "",
            "TR 0",
            "",
            "QS  \"R_NonBoundary\"           { *+* }",
            "QS  \"R_Silence\"                      { *+sil }",
            "QS  \"R_Stop\"              { *+p,*+pd,*+b,*+t,*+td,*+d,*+dd,*+k,*+kd,*+g }",
            "QS  \"R_Nasal\"                        { *+m,*+n,*+en,*+ng }",
            "QS  \"R_Fricative\"      { *+s,*+sh,*+z,*+f,*+v,*+ch,*+jh,*+th,*+dh }",
            "QS  \"R_Liquid\"                       { *+l,*+el,*+r,*+w,*+y,*+hh }",
            "QS  \"R_Vowel\"                       { *+eh,*+ih,*+ao,*+aa,*+uw,*+ah,*+ax,*+er,*+ar,*+ir,*+ur,*+ay,*+oy,*+ey,*+iy,*+ow }",
            "QS  \"R_C-Front\"                    { *+p,*+pd,*+b,*+m,*+f,*+v,*+w }",
            "QS  \"R_C-Central\"     { *+t,*+td,*+d,*+dd,*+en,*+n,*+s,*+z,*+sh,*+th,*+dh,*+l,*+el,*+r }",
            "QS  \"R_C-Back\"                     { *+sh,*+ch,*+jh,*+y,*+k,*+kd,*+g,*+ng,*+hh }",
            "QS  \"R_V-Front\"                    { *+iy,*+ih,*+eh }",
            "QS  \"R_V-Central\"     { *+eh,*+aa,*+er,*+ar,*+ir,*+ur,*+ao }",
            "QS  \"R_V-Back\"                     { *+uw,*+aa,*+ax,*+uh }",
            "QS  \"R_Front\"                        { *+p,*+pd,*+b,*+m,*+f,*+v,*+w,*+iy,*+ih,*+eh }",
            "QS  \"R_Central\"                     { *+t,*+td,*+d,*+dd,*+en,*+n,*+s,*+z,*+sh,*+th,*+dh,*+l,*+el,*+r,*+eh,*+aa,*+er,*+ar,*+ir,*+ur,*+ao }",
            "QS  \"R_Back\"             { *+sh,*+ch,*+jh,*+y,*+k,*+kd,*+g,*+ng,*+hh,*+aa,*+uw,*+ax,*+uh }",
            "QS  \"R_Fortis\"                       { *+p,*+pd,*+t,*+td,*+k,*+kd,*+f,*+th,*+s,*+sh,*+ch }",
            "QS  \"R_Lenis\"             { *+b,*+d,*+dd,*+g,*+v,*+dh,*+z,*+sh,*+jh }",
            "QS  \"R_UnFortLenis\"            { *+m,*+n,*+en,*+ng,*+hh,*+l,*+el,*+r,*+y,*+w }",
            "QS  \"R_Coronal\"                    { *+t,*+td,*+d,*+dd,*+n,*+en,*+th,*+dh,*+s,*+z,*+sh,*+ch,*+jh,*+l,*+el,*+r }",
            "QS  \"R_NonCoronal\" { *+p,*+pd,*+b,*+m,*+k,*+kd,*+g,*+ng,*+f,*+v,*+hh,*+y,*+w }",
            "QS  \"R_Anterior\"        { *+p,*+pd,*+b,*+m,*+t,*+td,*+d,*+dd,*+n,*+en,*+f,*+v,*+th,*+dh,*+s,*+z,*+l,*+el,*+w }",
            "QS  \"R_NonAnterior\" { *+k,*+kd,*+g,*+ng,*+sh,*+hh,*+ch,*+jh,*+r,*+y }",
            "QS  \"R_Continuent\"   { *+m,*+n,*+en,*+ng,*+f,*+v,*+th,*+dh,*+s,*+z,*+sh,*+hh,*+l,*+el,*+r,*+y,*+w }",
            "QS  \"R_NonContinuent\"        { *+p,*+pd,*+b,*+t,*+td,*+d,*+dd,*+k,*+kd,*+g,*+ch,*+jh }",
            "QS  \"R_Strident\"        { *+s,*+z,*+sh,*+ch,*+jh }",
            "QS  \"R_NonStrident\"  { *+f,*+v,*+th,*+dh,*+hh }",
            "QS  \"R_UnStrident\"   { *+p,*+pd,*+b,*+m,*+t,*+td,*+d,*+dd,*+n,*+en,*+k,*+kd,*+g,*+ng,*+l,*+el,*+r,*+y,*+w }",
            "QS  \"R_Glide\"                        { *+hh,*+l,*+el,*+r,*+y,*+w }",
            "QS  \"R_Syllabic\"         { *+en,*+m,*+l,*+el,*+er,*+ar,*+ir,*+ur }",
            "QS  \"R_Unvoiced-Cons\"        { *+p,*+pd,*+t,*+td,*+k,*+kd,*+s,*+sh,*+f,*+th,*+hh,*+ch }",
            "QS  \"R_Voiced-Cons\" { *+jh,*+b,*+d,*+dd,*+dh,*+g,*+y,*+l,*+el,*+m,*+n,*+en,*+ng,*+r,*+v,*+w,*+z }",
            "QS  \"R_Unvoiced-All\"            { *+p,*+pd,*+t,*+td,*+k,*+kd,*+s,*+sh,*+f,*+th,*+hh,*+ch,*+sil }",
            "QS  \"R_Long\"             { *+iy,*+aa,*+ow,*+ao,*+uw,*+en,*+m,*+l,*+el }",
            "QS  \"R_Short\"                        { *+eh,*+ey,*+aa,*+ih,*+ay,*+oy,*+ah,*+ax,*+uh }",
            "QS  \"R_Dipthong\"      { *+ey,*+ay,*+oy,*+aa,*+er,*+ar,*+ir,*+ur,*+en,*+m,*+l,*+el }",
            "QS  \"R_Front-Start\"   { *+ey,*+aa,*+er,*+ar,*+ir,*+ur }",
            "QS  \"R_Fronting\"       { *+ay,*+ey,*+oy }",
            "QS  \"R_High\"             { *+ih,*+uw,*+aa,*+ax,*+iy }",
            "QS  \"R_Medium\"                    { *+ey,*+er,*+ar,*+ir,*+ur,*+aa,*+ax,*+eh,*+en,*+m,*+l,*+el }",
            "QS  \"R_Low\"              { *+eh,*+ay,*+aa,*+aw,*+ao,*+oy }",
            "QS  \"R_Rounded\"                   { *+ao,*+uw,*+aa,*+ax,*+oy,*+w }",
            "QS  \"R_Unrounded\"   { *+eh,*+ih,*+aa,*+er,*+ar,*+ir,*+ur,*+ay,*+ey,*+iy,*+aw,*+ah,*+ax,*+en,*+m,*+hh,*+l,*+el,*+r,*+y }",
            "QS  \"R_NonAffricate\"            { *+s,*+sh,*+z,*+f,*+v,*+th,*+dh }",
            "QS  \"R_Affricate\"       { *+ch,*+jh }",
            "QS  \"R_IVowel\"                      { *+ih,*+iy }",
            "QS  \"R_EVowel\"                     { *+eh,*+ey }",
            "QS  \"R_AVowel\"                     { *+eh,*+aa,*+er,*+ar,*+ir,*+ur,*+ay,*+aw }",
            "QS  \"R_OVowel\"                    { *+ao,*+oy,*+aa }",
            "QS  \"R_UVowel\"                    { *+aa,*+ax,*+en,*+m,*+l,*+el,*+uw }",
            "QS  \"R_Voiced-Stop\"  { *+b,*+d,*+dd,*+g }",
            "QS  \"R_Unvoiced-Stop\"         { *+p,*+pd,*+t,*+td,*+k,*+kd }",
            "QS  \"R_Front-Stop\"   { *+p,*+pd,*+b }",
            "QS  \"R_Central-Stop\"            { *+t,*+td,*+d,*+dd }",
            "QS  \"R_Back-Stop\"     { *+k,*+kd,*+g }",
            "QS  \"R_Voiced-Fric\"  { *+z,*+sh,*+dh,*+ch,*+v }",
            "QS  \"R_Unvoiced-Fric\"          { *+s,*+sh,*+th,*+f,*+ch }",
            "QS  \"R_Front-Fric\"    { *+f,*+v }",
            "QS  \"R_Central-Fric\" { *+s,*+z,*+th,*+dh }",
            "QS  \"R_Back-Fric\"     { *+sh,*+ch,*+jh }",
            "QS  \"R_aa\"                 { *+aa }",
            "QS  \"R_ae\"                 { *+ae }",
            "QS  \"R_ah\"                 { *+ah }",
            "QS  \"R_ao\"                 { *+ao }",
            "QS  \"R_aw\"                { *+aw }",
            "QS  \"R_ax\"                 { *+ax }",
            "QS  \"R_ay\"                 { *+ay }",
            "QS  \"R_b\"                   { *+b }",
            "QS  \"R_ch\"                 { *+ch }",
            "QS  \"R_d\"                   { *+d }",
            "QS  \"R_dd\"                 { *+dd }",
            "QS  \"R_dh\"                 { *+dh }",
            "QS  \"R_dx\"                 { *+dx }",
            "QS  \"R_eh\"                 { *+eh }",
            "QS  \"R_el\"                  { *+el }",
            "QS  \"R_en\"                 { *+en }",
            "QS  \"R_er\"                  { *+er }",
            "QS  \"R_ar\"                  { *+ar }",
            "QS  \"R_ir\"                  { *+ir }",
            "QS  \"R_ur\"                  { *+ur }",
            "QS  \"R_ey\"                  { *+ey }",
            "QS  \"R_f\"                    { *+f }",
            "QS  \"R_g\"                   { *+g }",
            "QS  \"R_hh\"                 { *+hh }",
            "QS  \"R_ih\"                  { *+ih }",
            "QS  \"R_iy\"                  { *+iy }",
            "QS  \"R_jh\"                  { *+jh }",
            "QS  \"R_k\"                   { *+k }",
            "QS  \"R_kd\"                 { *+kd }",
            "QS  \"R_l\"                    { *+l }",
            "QS  \"R_m\"                  { *+m }",
            "QS  \"R_n\"                   { *+n }",
            "QS  \"R_ng\"                 { *+ng }",
            "QS  \"R_ow\"                { *+ow }",
            "QS  \"R_oy\"                 { *+oy }",
            "QS  \"R_p\"                   { *+p }",
            "QS  \"R_pd\"                 { *+pd }",
            "QS  \"R_r\"                    { *+r }",
            "QS  \"R_s\"                    { *+s }",
            "QS  \"R_sh\"                  { *+sh }",
            "QS  \"R_t\"                    { *+t }",
            "QS  \"R_td\"                  { *+td }",
            "QS  \"R_th\"                  { *+th }",
            "QS  \"R_ts\"                  { *+ts }",
            "QS  \"R_uh\"                 { *+uh }",
            "QS  \"R_uw\"                { *+uw }",
            "QS  \"R_v\"                   { *+v }",
            "QS  \"R_w\"                  { *+w }",
            "QS  \"R_y\"                   { *+y }",
            "QS  \"R_z\"                    { *+z }",
            "QS  \"L_NonBoundary\"           { *-* }",
            "QS  \"L_Silence\"                      { sil-* }",
            "QS  \"L_Stop\"              { p-*,pd-*,b-*,t-*,td-*,d-*,dd-*,k-*,kd-*,g-* }",
            "QS  \"L_Nasal\"                        { m-*,n-*,en-*,ng-* }",
            "QS  \"L_Fricative\"      { s-*,sh-*,z-*,f-*,v-*,ch-*,jh-*,th-*,dh-* }",
            "QS  \"L_Liquid\"                       { l-*,el-*,r-*,w-*,y-*,hh-* }",
            "QS  \"L_Vowel\"                       { eh-*,ih-*,ao-*,aa-*,uw-*,ah-*,ax-*,er-*,ar-*,ir-*,ur-*,ay-*,oy-*,ey-*,iy-*,ow-* }",
            "QS  \"L_C-Front\"                    { p-*,pd-*,b-*,m-*,f-*,v-*,w-* }",
            "QS  \"L_C-Central\"     { t-*,td-*,d-*,dd-*,en-*,n-*,s-*,z-*,sh-*,th-*,dh-*,l-*,el-*,r-* }",
            "QS  \"L_C-Back\"                     { sh-*,ch-*,jh-*,y-*,k-*,kd-*,g-*,ng-*,hh-* }",
            "QS  \"L_V-Front\"                    { iy-*,ih-*,eh-* }",
            "QS  \"L_V-Central\"     { eh-*,aa-*,er-*,ar-*,ir-*,ur-*,ao-* }",
            "QS  \"L_V-Back\"                     { uw-*,aa-*,ax-*,uh-* }",
            "QS  \"L_Front\"                        { p-*,pd-*,b-*,m-*,f-*,v-*,w-*,iy-*,ih-*,eh-* }",
            "QS  \"L_Central\"                     { t-*,td-*,d-*,dd-*,en-*,n-*,s-*,z-*,sh-*,th-*,dh-*,l-*,el-*,r-*,eh-*,aa-*,er-*,ar-*,ir-*,ur-*,ao-* }",
            "QS  \"L_Back\"             { sh-*,ch-*,jh-*,y-*,k-*,kd-*,g-*,ng-*,hh-*,aa-*,uw-*,ax-*,uh-* }",
            "QS  \"L_Fortis\"                       { p-*,pd-*,t-*,td-*,k-*,kd-*,f-*,th-*,s-*,sh-*,ch-* }",
            "QS  \"L_Lenis\"             { b-*,d-*,dd-*,g-*,v-*,dh-*,z-*,sh-*,jh-* }",
            "QS  \"L_UnFortLenis\"            { m-*,n-*,en-*,ng-*,hh-*,l-*,el-*,r-*,y-*,w-* }",
            "QS  \"L_Coronal\"                    { t-*,td-*,d-*,dd-*,n-*,en-*,th-*,dh-*,s-*,z-*,sh-*,ch-*,jh-*,l-*,el-*,r-* }",
            "QS  \"L_NonCoronal\" { p-*,pd-*,b-*,m-*,k-*,kd-*,g-*,ng-*,f-*,v-*,hh-*,y-*,w-* }",
            "QS  \"L_Anterior\"        { p-*,pd-*,b-*,m-*,t-*,td-*,d-*,dd-*,n-*,en-*,f-*,v-*,th-*,dh-*,s-*,z-*,l-*,el-*,w-* }",
            "QS  \"L_NonAnterior\" { k-*,kd-*,g-*,ng-*,sh-*,hh-*,ch-*,jh-*,r-*,y-* }",
            "QS  \"L_Continuent\"   { m-*,n-*,en-*,ng-*,f-*,v-*,th-*,dh-*,s-*,z-*,sh-*,hh-*,l-*,el-*,r-*,y-*,w-* }",
            "QS  \"L_NonContinuent\"        { p-*,pd-*,b-*,t-*,td-*,d-*,dd-*,k-*,kd-*,g-*,ch-*,jh-* }",
            "QS  \"L_Strident\"        { s-*,z-*,sh-*,ch-*,jh-* }",
            "QS  \"L_NonStrident\"  { f-*,v-*,th-*,dh-*,hh-* }",
            "QS  \"L_UnStrident\"   { p-*,pd-*,b-*,m-*,t-*,td-*,d-*,dd-*,n-*,en-*,k-*,kd-*,g-*,ng-*,l-*,el-*,r-*,y-*,w-* }",
            "QS  \"L_Glide\"                        { hh-*,l-*,el-*,r-*,y-*,w-* }",
            "QS  \"L_Syllabic\"         { en-*,m-*,l-*,el-*,er-*,ar-*,ir-*,ur-* }",
            "QS  \"L_Unvoiced-Cons\"        { p-*,pd-*,t-*,td-*,k-*,kd-*,s-*,sh-*,f-*,th-*,hh-*,ch-* }",
            "QS  \"L_Voiced-Cons\" { jh-*,b-*,d-*,dd-*,dh-*,g-*,y-*,l-*,el-*,m-*,n-*,en-*,ng-*,r-*,v-*,w-*,z-* }",
            "QS  \"L_Unvoiced-All\"            { p-*,pd-*,t-*,td-*,k-*,kd-*,s-*,sh-*,f-*,th-*,hh-*,ch-*,sil-* }",
            "QS  \"L_Long\"             { iy-*,aa-*,ow-*,ao-*,uw-*,en-*,m-*,l-*,el-* }",
            "QS  \"L_Short\"                        { eh-*,ey-*,aa-*,ih-*,ay-*,oy-*,ah-*,ax-*,uh-* }",
            "QS  \"L_Dipthong\"      { ey-*,ay-*,oy-*,aa-*,er-*,ar-*,ir-*,ur-*,en-*,m-*,l-*,el-* }",
            "QS  \"L_Front-Start\"   { ey-*,aa-*,er-*,ar-*,ir-*,ur-* }",
            "QS  \"L_Fronting\"       { ay-*,ey-*,oy-* }",
            "QS  \"L_High\"             { ih-*,uw-*,aa-*,ax-*,iy-* }",
            "QS  \"L_Medium\"                    { ey-*,er-*,ar-*,ir-*,ur-*,aa-*,ax-*,eh-*,en-*,m-*,l-*,el-* }",
            "QS  \"L_Low\"              { eh-*,ay-*,aa-*,aw-*,ao-*,oy-* }",
            "QS  \"L_Rounded\"                   { ao-*,uw-*,aa-*,ax-*,oy-*,w-* }",
            "QS  \"L_Unrounded\"   { eh-*,ih-*,aa-*,er-*,ar-*,ir-*,ur-*,ay-*,ey-*,iy-*,aw-*,ah-*,ax-*,en-*,m-*,hh-*,l-*,el-*,r-*,y-* }",
            "QS  \"L_NonAffricate\"            { s-*,sh-*,z-*,f-*,v-*,th-*,dh-* }",
            "QS  \"L_Affricate\"       { ch-*,jh-* }",
            "QS  \"L_IVowel\"                      { ih-*,iy-* }",
            "QS  \"L_EVowel\"                     { eh-*,ey-* }",
            "QS  \"L_AVowel\"                     { eh-*,aa-*,er-*,ar-*,ir-*,ur-*,ay-*,aw-* }",
            "QS  \"L_OVowel\"                    { ao-*,oy-*,aa-* }",
            "QS  \"L_UVowel\"                    { aa-*,ax-*,en-*,m-*,l-*,el-*,uw-* }",
            "QS  \"L_Voiced-Stop\"  { b-*,d-*,dd-*,g-* }",
            "QS  \"L_Unvoiced-Stop\"         { p-*,pd-*,t-*,td-*,k-*,kd-* }",
            "QS  \"L_Front-Stop\"   { p-*,pd-*,b-* }",
            "QS  \"L_Central-Stop\"            { t-*,td-*,d-*,dd-* }",
            "QS  \"L_Back-Stop\"     { k-*,kd-*,g-* }",
            "QS  \"L_Voiced-Fric\"  { z-*,sh-*,dh-*,ch-*,v-* }",
            "QS  \"L_Unvoiced-Fric\"          { s-*,sh-*,th-*,f-*,ch-* }",
            "QS  \"L_Front-Fric\"    { f-*,v-* }",
            "QS  \"L_Central-Fric\" { s-*,z-*,th-*,dh-* }",
            "QS  \"L_Back-Fric\"     { sh-*,ch-*,jh-* }",
            "QS  \"L_aa\"                 { aa-* }",
            "QS  \"L_ae\"                 { ae-* }",
            "QS  \"L_ah\"                 { ah-* }",
            "QS  \"L_ao\"                 { ao-* }",
            "QS  \"L_aw\"                { aw-* }",
            "QS  \"L_ax\"                 { ax-* }",
            "QS  \"L_ay\"                 { ay-* }",
            "QS  \"L_b\"                   { b-* }",
            "QS  \"L_ch\"                 { ch-* }",
            "QS  \"L_d\"                   { d-* }",
            "QS  \"L_dd\"                 { dd-* }",
            "QS  \"L_dh\"                 { dh-* }",
            "QS  \"L_dx\"                 { dx-* }",
            "QS  \"L_eh\"                 { eh-* }",
            "QS  \"L_el\"                  { el-* }",
            "QS  \"L_en\"                 { en-* }",
            "QS  \"L_er\"                  { er-* }",
            "QS  \"L_ar\"                  { ar-* }",
            "QS  \"L_ir\"                  { ir-* }",
            "QS  \"L_ur\"                  { ur-* }",
            "QS  \"L_ey\"                  { ey-* }",
            "QS  \"L_f\"                    { f-* }",
            "QS  \"L_g\"                   { g-* }",
            "QS  \"L_hh\"                 { hh-* }",
            "QS  \"L_ih\"                  { ih-* }",
            "QS  \"L_iy\"                  { iy-* }",
            "QS  \"L_jh\"                  { jh-* }",
            "QS  \"L_k\"                   { k-* }",
            "QS  \"L_kd\"                 { kd-* }",
            "QS  \"L_l\"                    { l-* }",
            "QS  \"L_m\"                  { m-* }",
            "QS  \"L_n\"                   { n-* }",
            "QS  \"L_ng\"                 { ng-* }",
            "QS  \"L_ow\"                { ow-* }",
            "QS  \"L_oy\"                 { oy-* }",
            "QS  \"L_p\"                   { p-* }",
            "QS  \"L_pd\"                 { pd-* }",
            "QS  \"L_r\"                    { r-* }",
            "QS  \"L_s\"                    { s-* }",
            "QS  \"L_sh\"                  { sh-* }",
            "QS  \"L_t\"                    { t-* }",
            "QS  \"L_td\"                  { td-* }",
            "QS  \"L_th\"                  { th-* }",
            "QS  \"L_ts\"                  { ts-* }",
            "QS  \"L_uh\"                 { uh-* }",
            "QS  \"L_uw\"                { uw-* }",
            "QS  \"L_v\"                   { v-* }",
            "QS  \"L_w\"                  { w-* }",
            "QS  \"L_y\"                   { y-* }",
            "QS  \"L_z\"                    { z-* }",
            " ",
            "TR 2",
            ""
        };

        static List<string> mkTriLed = new List<string>()
            {
                "WB sp",
                "WB sil",
                "TC"
            };

        static List<string> config = new List<string>()
            {
                "TARGETKIND = MFCC_0_D_N_Z",
                "TARGETRATE = 100000.0",
                "SAVECOMPRESSED = T",
                "SAVEWITHCRC = T",
                "WINDOWSIZE = 250000.0",
                "USEHAMMING = T",
                "PREEMCOEF = 0.97",
                "NUMCHANS = 26",
                "CEPLIFTER = 22",
                "NUMCEPS = 12"
            };

        static List<string> wav_config = new List<string>()
            {
                "SOURCEFORMAT = WAV",
                "TARGETKIND = MFCC_0_D",
                "TARGETRATE = 100000.0",
                "SAVECOMPRESSED = T",
                "SAVEWITHCRC = T",
                "WINDOWSIZE = 250000.0",
                "USEHAMMING = T",
                "PREEMCOEF = 0.97",
                "NUMCHANS = 26",
                "CEPLIFTER = 22",
                "NUMCEPS = 12"
            };

        static List<string> silHed = new List<string>()
            {
                "AT 2 4 0.2 {sil.transP}",
                "AT 4 2 0.2 {sil.transP}",
                "AT 1 3 0.3 {sp.transP}",
                "TI silst {sil.state[3],sp.state[2]}"
            };

        static List<string> jConf = new List<string>()
        {
            "#",
            "# Sample Jconf configuration file",
            "# for Julius library rev.4.3",
            "######################################################################",
            "",
            "####",
            "#### misc.",
            "####",
            "# !!!!!! VoxForge change",
            "#-outprobout filename\t\t# save computed outprob vectors to HTK file (for debug)",
            "# !!!!!! ",
            "",
            "# VoxForge configurations:",
            "-dfa ../" + projectName + "/" + projectName + ".dfa     # finite state automaton grammar file",
            "-v ../" + projectName + "/" + projectName + ".dict      # pronunciation dictionary",
            "-h ../" + projectName + "/hmm15/hmmdefs    # acoustic HMM (ascii or Julius binary)",
            "-hlist ../" + projectName + "/tiedlist     # HMMList to map logical phone to physical",
            "-smpFreq 16000\t    # sampling rate (Hz)",
            "-spmodel \"sp\"\t\t    # name of a short-pause silence model",
            "-multipath          # force enable MULTI-PATH model handling",
            "-gprune safe        # Gaussian pruning method",
            "-iwcd1 max          # Inter-word triphone approximation method",
            "-iwsppenalty -70.0\t# transition penalty for the appended sp models",
            "-iwsp\t\t\t          # append a skippable sp model at all word ends",
            "-penalty1 5.0\t\t    # word insertion penalty for grammar (pass1)",
            "-penalty2 20.0\t    # word insertion penalty for grammar (pass2)",
            "-b2 200             # beam width on 2nd pass (#words)",
            "-sb 200.0\t\t        # score beam envelope threshold",
            "-n 1                # num of sentences to find",
            "",
            "# you may need to adjust your \"-lv\" value to prevent the recognizer inadvertently ",
            "# recognizing non-speech sounds:",
            "-lv 4000\t\t\t# level threshold (0-32767)",
            "",
            "# comment these out for debugging:",
            "-logfile ../julius/julius.log",
            "-quiet",
            "# !!!!!!",
            "",
            "",
        };

        static string[] spCharList;

        static bool reCreateWav_Mfc = false;

        static char[] emptyChar = new char[] { ' ' };

        static List<string> wavFiles = new List<string>();

        #endregion

        #region Main

        static void Main(string[] args)
        {
            #region HMM Training

            List<char> ExceptionLetters = GetLetters(fromTo);
            SetMappingToLetters(ExceptionLetters);
            Dictionary<string, string> data = CreatePromptFile(ExceptionLetters);
            List<string> lexicon = CreateWListFile(data);
            Dictionary<string, string> lexiconData = CreateLexicon(lexicon);
            List<string> mono = CreateMonoPhone();
            List<string> wordsMlf = CreateWordsMLF(data);
            CreatePhone(wordsMlf);
            if (!Directory.Exists(path + @"train\wav") || reCreateWav_Mfc)
            {
                CreateMFCC(data.Keys.ToList());
            }
            CreateProto(data.Keys.ToList());
            CreateHMM(mono);
            CreateTriPhones(lexiconData, mono);

            #endregion

            #region Julius Testing

            CreateGrammarVoca(lexiconData);

            CreateConfig();

            StartRecognizing();

            #endregion
        }

        #endregion

        #region Private Methods

        private static void StartRecognizing(bool useMic = false, bool writeToFile = true)
        {
            CreateDirectory(dataPath + "julius");
            string quiet = "";
            if (writeToFile)
            {
                quiet = " -quiet > ../julius/" + projectName + ".txt";
                Console.WriteLine("Result saved to this path : " + dataPath + "julius\\" + projectName + ".txt");
            }
            if (useMic)
            {
                RunCommand(@"/c julius-4.3.1 -input mic -C ../manual/" + projectName + ".jconf" + quiet);
            }
            else
            {
                RunCommand(@"/c julius-4.3.1 -input rawfile -C ../manual/" + projectName + ".jconf" + quiet);
            }
        }

        private static void CreateGrammarVoca(Dictionary<string, string> lexiconData)
        {
            List<string> grammar = new List<string>()
            {
                "S : NS_B SENT NS_E"
            };
            List<string> voca = new List<string>()
            {
                "% NS_B",
                "<s>        sil",
                "",
                "% NS_E",
                "</s>        sil",
                "",
            };
            grammar.Add("SENT: " + projectName);
            voca.Add("% " + projectName);
            foreach (var item in lexiconData)
            {
                if (item.Value != "sil" && item.Value != "sp")
                {
                    voca.Add(item.Key + "\t" + item.Value);
                }
            }

            File.WriteAllLines(path + projectName + ".grammar", grammar);
            File.WriteAllLines(path + projectName + ".voca", voca);

            RunCommand(@"/c julia ../mkdfa.jl " + projectName);
        }

        private static void CreateConfig()
        {
            CreateDirectory(dataPath + "manual");
            File.WriteAllLines(dataPath + "manual\\" + projectName + ".jconf", jConf);
        }

        private static void CreateTriPhones(Dictionary<string, string> data, List<string> mono)
        {
            File.WriteAllLines(path + "mktri.led", mkTriLed);

            RunCommand(@"/c HLEd -A -D -T 1 -n triphones1 -l * -i wintri.mlf mktri.led aligned.mlf");

            CreateDirectory(path + "hmm10");
            CreateDirectory(path + "hmm11");
            CreateDirectory(path + "hmm12");

            List<string> mktriHed = new List<string>()
            {
                "CL triphones1"
            };
            mktriHed.Add(string.Format("TI T_{0} {{(*-{0}+*,{0}+*,*-{0}).transP}}", "sp"));
            mktriHed.AddRange(mono.Select(a => string.Format("TI T_{0} {{(*-{0}+*,{0}+*,*-{0}).transP}}", a)));
            File.WriteAllLines(path + "mktri.hed", mktriHed);

            RunCommand(@"/c HHEd -A -D -T 1 -H hmm9/macros -H hmm9/hmmdefs -M hmm10 mktri.hed monophones1");
            RunCommand(@"/c HERest  -A -D -T 1 -C config -I wintri.mlf -t 250.0 150.0 3000.0 -S train.scp -H hmm10/macros -H hmm10/hmmdefs -M hmm11 triphones1");
            RunCommand(@"/c HERest  -A -D -T 1 -C config -I wintri.mlf -t 250.0 150.0 3000.0 -s stats -S train.scp -H hmm11/macros -H hmm11/hmmdefs -M hmm12 triphones1");

            var keys = data.Keys.ToList();
            var values = data.Values.ToList();
            Dictionary<string, string> maketriphones = new Dictionary<string, string>();
            for (int j = 0; j < keys.Count; j++)
            {
                var currentValueList = values[j].Split(emptyChar, StringSplitOptions.RemoveEmptyEntries).ToList();
                string currentValue = "";
                if (currentValueList.Contains("sil"))
                {
                    currentValue = "sil";
                }
                else
                {
                    if (currentValueList.Count > 1)
                    {
                        for (int i = 0; i < currentValueList.Count; i++)
                        {
                            if (i == 0)
                            {
                                currentValue += currentValueList[i] + "+" + currentValueList[i + 1] + " ";
                            }
                            else if (i < currentValueList.Count - 1)
                            {
                                currentValue += currentValueList[i - 1] + "-" + currentValueList[i] + "+" + currentValueList[i + 1] + " ";
                            }
                            else
                            {
                                currentValue += currentValueList[i - 1] + "-" + currentValueList[i] + " sp";
                            }
                        }
                    }
                    else
                    {
                        currentValue = string.Join(" ", currentValueList) + " sp";
                    }
                }
                maketriphones.Add(keys[j], currentValue);
            }

            IEnumerable<string> fullList0 = maketriphones.Values.SelectMany(a => a.Split(emptyChar, StringSplitOptions.RemoveEmptyEntries)).Distinct();
            IEnumerable<string> dictTri = maketriphones.Select(a => a.Key + "\t" + a.Value);
            File.WriteAllLines(path + "fulllist0", fullList0);
            File.WriteAllLines(path + "dict-tri", dictTri);

            CreateDirectory(path + "hmm13");
            CreateDirectory(path + "hmm14");
            CreateDirectory(path + "hmm15");

            List<string> distinctList = new List<string>(fullList0);
            distinctList.AddRange(mono);
            File.WriteAllLines(path + "fulllist", distinctList.Distinct());

            File.WriteAllLines(path + "tree1.hed", TreeHed);

            File.Copy(path + "tree1.hed", path + "tree.hed", true);

            List<string> _treeHed = new List<string>(TreeHed);
            for (int i = 2; i < 5; i++)
            {
                _treeHed.AddRange(mono.Select(a => string.Format("TB 350 \"ST_{0}_{1}_\" {{(\"{0}\",\"*-{0}+*\",\"{0}+*\",\"*-{0}\").state[{1}]}}", a, i)));
            }
            _treeHed.Add("");
            _treeHed.Add("TR 1");
            _treeHed.Add("");
            _treeHed.Add("AU \"./fulllist\"");
            _treeHed.Add("CO \"./tiedlist\"");
            _treeHed.Add("");
            _treeHed.Add("ST \"./trees\"");

            File.WriteAllLines(path + "tree.hed", _treeHed);

            RunCommand(@"/c HHEd -A -D -T 1 -H hmm12/macros -H hmm12/hmmdefs -M hmm13 tree.hed triphones1");
            RunCommand(@"/c HERest -A -D -T 1 -T 1 -C config -I wintri.mlf  -t 250.0 150.0 3000.0 -S train.scp -H hmm13/macros -H hmm13/hmmdefs -M hmm14 tiedlist");
            RunCommand(@"/c HERest -A -D -T 1 -T 1 -C config -I wintri.mlf  -t 250.0 150.0 3000.0 -S train.scp -H hmm14/macros -H hmm14/hmmdefs -M hmm15 tiedlist");
        }

        private static void CreateProto(List<string> fileNames)
        {
            CreateDirectory(path + "hmm0");
            File.WriteAllLines(path + "proto", proto);
            List<string> train = new List<string>();
            foreach (var item in fileNames)
            {
                train.Add("train/mfcc/" + item + ".mfc");
            }
            File.WriteAllLines(path + "train.scp", train);
            File.WriteAllLines(path + "config", config);
            RunCommand("/c HCompV -A -D -T 1 -C config -f 0.01 -m -S train.scp -M hmm0 proto");
        }

        private static void CreateMFCC(List<string> fileNames)
        {
            CreateDirectory(path + @"train\wav");
            CreateDirectory(path + @"train\mfcc");
            foreach (var item in wavFiles)
            {
                File.Copy(item, path + "train\\wav\\" + GetFileName(item, true));
            }
            List<string> train = new List<string>();
            foreach (var item in fileNames)
            {
                train.Add("train/wav/" + item + ".wav train/mfcc/" + item + ".mfc");
            }

            File.WriteAllLines(path + "codetrain.scp", train);
            File.WriteAllLines(path + "wav_config", wav_config);
            RunCommand(@"/c HCopy -A -D -T 1 -C wav_config -S codetrain.scp");
        }

        private static void CreateHMM(List<string> mono)
        {
            var protohmm0 = File.ReadAllLines(path + @"hmm0\proto");
            var vFloors = File.ReadAllLines(path + @"hmm0\vFloors");
            var hmm = protohmm0.Skip(4).ToList();
            var macro = protohmm0.Take(3).ToList();
            macro.AddRange(vFloors);
            List<string> hmmdefs = new List<string>();
            foreach (var item in mono)
            {
                hmmdefs.Add("~h \"" + item + "\"");
                hmmdefs.AddRange(hmm);
            }
            File.WriteAllLines(path + @"hmm0\hmmdefs", hmmdefs);
            File.WriteAllLines(path + @"hmm0\macros", macro);
            for (int i = 0; i < 9; i++)
            {
                CreateDirectory(path + "hmm" + (i + 1));
            }

            RunCommand(@"/c HERest -A -D -T 1 -C config -I phones0.mlf -t 250.0 150.0 1000.0 -S train.scp -H hmm0/macros -H hmm0/hmmdefs -M hmm1 monophones0");
            RunCommand(@"/c HERest -A -D -T 1 -C config -I phones0.mlf -t 250.0 150.0 1000.0 -S train.scp -H hmm1/macros -H hmm1/hmmdefs -M hmm2 monophones0");
            RunCommand(@"/c HERest -A -D -T 1 -C config -I phones0.mlf -t 250.0 150.0 1000.0 -S train.scp -H hmm2/macros -H hmm2/hmmdefs -M hmm3 monophones0");

            foreach (var item in Directory.GetFiles(path + "hmm3"))
            {
                string name = GetFileName(item, true);
                File.Copy(item, item.Replace(path + "hmm3", path + "hmm4"), true);
                if (name == "hmmdefs")
                {
                    List<string> lines = File.ReadAllLines(item).ToList();
                    int startIndex = lines.IndexOf("~h \"sil\"");
                    int endIndex = lines.IndexOf("<ENDHMM>", startIndex);
                    List<string> newSP = lines.GetRange(startIndex, endIndex - startIndex + 1);
                    int NumStateIndex = newSP.FindIndex(a => a.Contains("<NUMSTATES>"));
                    int StateIndex1 = newSP.FindIndex(a => a.Contains("<STATE>"));
                    int StateIndex2 = newSP.FindIndex(StateIndex1 + 1, a => a.Contains("<STATE>"));
                    int StateIndex3 = newSP.FindIndex(StateIndex2 + 1, a => a.Contains("<STATE>"));
                    int TRANSP = newSP.FindIndex(a => a.Contains("<TRANSP>"));

                    newSP[0] = "~h \"sp\"";
                    newSP[NumStateIndex] = "<NUMSTATES> 3";
                    newSP[StateIndex2] = "<STATE> 2";
                    newSP[TRANSP] = "<TRANSP> 3";

                    newSP.RemoveRange(TRANSP + 1, (newSP.Count) - (TRANSP + 1));
                    newSP.RemoveRange(StateIndex3, TRANSP - StateIndex3);
                    newSP.RemoveRange(StateIndex1, StateIndex2 - StateIndex1);

                    newSP.Add("0.0 1.0 0.0");
                    newSP.Add("0.0 0.9 0.1");
                    newSP.Add("0.0 0.0 0.0");
                    newSP.Add("<ENDHMM>");

                    File.AppendAllLines(path + @"hmm4\" + name, newSP);
                }
            }

            File.WriteAllLines(path + "sil.hed", silHed);

            RunCommand(@"/c HHEd -A -D -T 1 -H hmm4/macros -H hmm4/hmmdefs -M hmm5 sil.hed monophones1");
            RunCommand(@"/c HERest -A -D -T 1 -C config  -I phones1.mlf -t 250.0 150.0 3000.0 -S train.scp -H hmm5/macros -H  hmm5/hmmdefs -M hmm6 monophones1");
            RunCommand(@"/c HERest -A -D -T 1 -C config  -I phones1.mlf -t 250.0 150.0 3000.0 -S train.scp -H hmm6/macros -H hmm6/hmmdefs -M hmm7 monophones1");

            RunCommand(@"/c HVite -A -D -T 1 -l * -o SWT -b SENT-END -C config -H hmm7/macros -H hmm7/hmmdefs -i aligned.mlf -m -t 250.0 150.0 1000.0 -y lab -a -I words.mlf -S train.scp dict monophones1> HVite_log");

            RunCommand(@"/c HERest -A -D -T 1 -C config -I aligned.mlf -t 250.0 150.0 3000.0 -S train.scp -H hmm7/macros -H hmm7/hmmdefs -M hmm8 monophones1");
            RunCommand(@"/c HERest -A -D -T 1 -C config -I aligned.mlf -t 250.0 150.0 3000.0 -S train.scp -H hmm8/macros -H hmm8/hmmdefs -M hmm9 monophones1");
        }

        private static void CreatePhone(List<string> wordsMlf)
        {
            List<string> tryG;
            List<string> phone0 = new List<string>();
            List<string> phone1 = new List<string>();
            phone0.AddRange(wordsMlf.Take(2));
            phone1.AddRange(wordsMlf.Take(2));
            phone0.Add("sil");
            phone1.Add("sil");
            int index = 2;
            foreach (string item in wordsMlf.Skip(2))
            {
                index++;
                if (item == ".")
                {
                    phone0.Add("sil");
                    phone0.Add(".");
                    phone1.Add("sil");
                    phone1.Add(".");
                    if (index < wordsMlf.Count)
                    {
                        phone0.Add(wordsMlf[index]);
                        phone1.Add(wordsMlf[index]);
                        phone0.Add("sil");
                        phone1.Add("sil");
                    }
                    continue;
                }
                foreach (var item1 in item)
                {
                    if (keyValues.TryGetValue(item1 + "", out tryG))
                    {
                        phone0.Add(tryG[0].Replace("[", "").Replace("]", ""));
                        phone1.Add(tryG[0].Replace("[", "").Replace("]", ""));
                    }
                }
                if (phone0[phone0.Count - 1] != "sil")
                {
                    phone0.Add("sp");
                }
            }
            File.WriteAllLines(path + "phones0.mlf", phone1);
            File.WriteAllLines(path + "phones1.mlf", phone0);
        }

        private static List<string> CreateWordsMLF(Dictionary<string, string> data)
        {
            List<string> list = new List<string>() { "#!MLF!#" };
            foreach (var item in data)
            {
                list.Add("\"*/" + item.Key + ".lab\"");
                list.AddRange(item.Value.Split(spCharList, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).Where(b => !string.IsNullOrWhiteSpace(b)).ToList());
                list.Add(".");
            }
            File.WriteAllLines(path + "words.mlf", list);
            return list;
        }

        private static List<string> CreateMonoPhone()
        {
            List<string> mono = keyValues.Values.SelectMany(a => a).Select(b => b.Replace("[", "").Replace("]", "")).Distinct().ToList();
            File.WriteAllLines(path + "monophones0", mono);
            mono.Insert(0, "sp");
            File.WriteAllLines(path + "monophones1", mono);
            mono.RemoveAt(0);
            return mono;
        }

        private static Dictionary<string, string> CreateLexicon(List<string> lexicon)
        {
            Dictionary<string, string> done = new Dictionary<string, string>();
            HashSet<char> err = new HashSet<char>();
            List<string> cur = new List<string>();
            int maxLen = 0;
            int maxLenIt = 0;
            foreach (string item in lexicon)
            {
                if (item == "SENT-START" || item == "SENT-END")
                {
                    done.Add(item, "sil");
                    continue;
                }
                cur.Clear();
                foreach (char item1 in item)
                {
                    try
                    {
                        if (cur.Count > 0)
                        {
                            List<string> temp = new List<string>();
                            foreach (string item3 in cur)
                            {
                                foreach (string item2 in keyValues[item1 + ""])
                                {
                                    temp.Add(item3 + item2 + " ");
                                }
                            }
                            cur.Clear();
                            if (temp.Count > 0)
                            {
                                cur.AddRange(temp);
                            }
                            else
                            {
                                //cur.Add(item2);
                            }
                        }
                        else
                        {
                            foreach (string item2 in keyValues[item1 + ""])
                            {
                                cur.Add(item2 + " ");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        err.Add(item1);
                    }
                }
                for (int i = 0; i < cur.Count; i++)
                {
                    string curStr = cur[i];
                    string curHea = (i == 0) ? item : item + "(" + (i + 1).ToString().PadLeft((cur.Count + "").Length, '0') + ")";
                    done.Add(curHea, curStr);
                    if (maxLen < curHea.Length)
                    {
                        maxLen = curHea.Length;
                    }
                    if (maxLenIt < curStr.Length)
                    {
                        maxLenIt = curStr.Length;
                    }
                }
            }
            string fmt = string.Format("{0," + maxLen + "}", " ");
            maxLen += 20;
            maxLenIt += 20;

            List<string> tabs = new List<string>();

            List<string> hhh = done.Select(a => a.Key + "\t[" + ((a.Key == "SENT-START" || a.Key == "SENT-END") ? ("]\t" + a.Value.Trim()) : (a.Key + "]\t" + a.Value.Trim() + "{0}"))).ToList();
            File.WriteAllLines(path + "dict", hhh.Select(a => string.Format(a, " sp")));
            if (err.Count > 0)
            {
                File.WriteAllLines(path + "unusedCharacters", err.Select(a => a + ""));
            }
            CreateDirectory(path + "lexicon");
            File.WriteAllLines(path + @"lexicon\VoxForgeDict.txt", hhh.Select(a => string.Format(a, "")));

            return done;
        }

        private static void CreateGlobalDed()
        {
            List<string> global = new List<string>()
            {
                "AS sp",
                "RS cmu",
                "MP sil sil sp",
                ""
            };
            File.WriteAllLines(path + "global.ded", global);
        }

        private static List<string> CreateWListFile(Dictionary<string, string> data)
        {
            string value = string.Join(spCharList[0], data.Values);
            List<string> wlist = value.Split(spCharList, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).Where(b => !string.IsNullOrWhiteSpace(b)).ToList();
            List<string> allUsedChars = wlist.SelectMany(a => a.ToCharArray()).Distinct().OrderBy(b => b).Select(c => c + "").ToList();
            allUsedChars.Add("SENT-START");
            allUsedChars.Add("SENT-END");
            List<string> allAvailChars = keyValues.Keys.ToList();
            allAvailChars.RemoveAll(a => allUsedChars.Contains(a));
            foreach (string item in allAvailChars)
            {
                keyValues.Remove(item);
            }
            wlist.Add("SENT-START");
            wlist.Add("SENT-END");
            wlist = wlist.Distinct().OrderBy(a => a).ToList();
            File.WriteAllLines(path + "wlist", wlist);
            return wlist;
        }

        private static Dictionary<string, string> CreatePromptFile(List<char> ExceptionLetters)
        {
            CreateDirectory(path);
            Dictionary<string, string> speechDataList = new Dictionary<string, string>();
            wavFiles.Clear();
            foreach (var item in Directory.EnumerateFiles(dataPath + "data\\label"))
            {
                string name = GetFileName(item);
                wavFiles.Add(dataPath + "data\\wav\\" + name + ".wav");
                string speechData = File.ReadAllText(item);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < speechData.Length; i++)
                {
                    if (ExceptionLetters.Contains(speechData[i]))
                    {
                        sb.Append(speechData[i]);
                        continue;
                    }
                    sb.Append(' ');
                }
                string joinedValue = sb.ToString();
                speechDataList.Add(name, joinedValue);
            }
            File.WriteAllLines(path + "prompts.txt", speechDataList.Select(a => "*/" + a.Key + " " + a.Value));
            return speechDataList;
        }

        private static IEnumerable<char> GetCharArray(int key, int value)
        {
            return Enumerable.Range(key, value - key + 1).Select(a => (char)a);
        }

        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static string GetFileName(string path, bool withExt = false)
        {
            string[] pathList = path.Split('\\');
            string name = pathList[pathList.Length - 1];
            if (withExt)
            {
                return name;
            }
            else
            {
                return name.Substring(0, name.LastIndexOf('.'));
            }
        }

        private static void RunCommand(string cmd)
        {
            Process p = new Process();
            p.StartInfo.WorkingDirectory = path;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = cmd;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForExit();
        }

        private static List<char> GetLetters(List<KeyValuePair<int, int>> fromTo)
        {
            List<char> ExceptionLetters = new List<char>();
            foreach (var item in fromTo)
            {
                ExceptionLetters.AddRange(GetCharArray(item.Key, item.Value));
            }
            return ExceptionLetters;
        }

        private static void SetMappingToLetters(List<char> ExceptionLetters)
        {
            if (!File.Exists(dataPath + @"data\dict\dict.txt"))
            {
                File.WriteAllLines(dataPath + @"data\dict\dict.txt", ExceptionLetters.Select(a => a + "\t"));
            }
            else
            {
                keyValues = File.ReadAllLines(dataPath + @"data\dict\dict.txt").
                    Where(c => !string.IsNullOrWhiteSpace(c.Split('\t')[1])).
                    ToDictionary(
                        a => a.Split('\t')[0],
                        b => new List<string>() { b.Split('\t')[1] }
                    );
                spCharList = keyValues.Where(a => a.Value.Contains("sp")).Select(b => b.Key).ToArray();
                foreach (var item in spCharList)
                {
                    keyValues.Remove(item);
                }
                keyValues.Add("SENT-END", new List<string>() { "sil" });
                keyValues.Add("SENT-START", new List<string>() { "sil" });
            }
        }

        #endregion
    }
}

