using MaimaiBasic.Classes.Song;
using MaimaiBasic.Enums;
using System.Buffers;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MaimaiBasic.Utils;
public class SongInfoConverter : JsonConverter<Dictionary<int, SongInfo>>
{
    private void WriteSingleChart(SingleChart sc, int[] notes, int level, string artist)
    {
        var note = new Note();
        note.Tap = notes[0];
        note.Hold = notes[1];
        note.Slide = notes[2];
        if (level > 4)
        {
            note.Touch = notes[3];
            note.Break = notes[4];
        }
        else
            note.Break = notes[3];
        sc.Note = note;
        sc.Artist = artist == "-" ? null : artist;
    }

    public override Dictionary<int, SongInfo> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dict = new Dictionary<int, SongInfo>();

        reader.Read();
        Debug.Assert(reader.CurrentDepth == 1);

        while (reader.TokenType != JsonTokenType.EndArray)
        {
            // start obj
            reader.Read();

            var songInfo = new SongInfo();
            //PropertyName id
            reader.Read();

            // String id,
            var id = int.Parse(reader.GetString()!);
            // PropertyName title
            reader.Read();

            songInfo.Id = id;
            // PropertyName title
            reader.Read();

            songInfo.Title = reader.GetString()!;
            // PropertyName type
            reader.Read();
            //string
            reader.Read();

            songInfo.Type = Enum.Parse<SongType>(reader.GetString()!);

            // PropertyName ds
            reader.Read();
            reader.Read();
            var dsArray = ArrayPool<double>.Shared.Rent(5);
            var levelArray = ArrayPool<string>.Shared.Rent(5);
            var cidArray = ArrayPool<int>.Shared.Rent(5);

            var i = 0;

            reader.Read();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                dsArray[i++] = reader.GetDouble();
                reader.Read();
            }
            reader.Read();

            i = 0;
            //    PropertyName level
            reader.Read();
            //     StartArray
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                levelArray[i++] = reader.GetString()!;
                reader.Read();
            }
            //    EndArray
            reader.Read();

            i = 0;
            //    PropertyName cids
            reader.Read();

            //    StartArray
            reader.Read();


            while (reader.TokenType != JsonTokenType.EndArray)
            {
                cidArray[i++] = reader.GetInt32();
                reader.Read();
            }
            //    EndArray
            reader.Read();
            //    PropertyName charts
            reader.Read();

            //charts
            i = 0;

            //StartArray
            reader.Read();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                //StartObject
                reader.Read();
                //charts.notes
                var notesArray = ArrayPool<int>.Shared.Rent(5);

                //PropertyName notes
                reader.Read();

                //StartArray
                reader.Read();
                int j = 0;
                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    notesArray[j++] = reader.GetInt32();
                    reader.Read();
                }
                // EndArray
                reader.Read();

                //PropertyName charter
                reader.Read();

                if (i == 0)
                    WriteSingleChart(songInfo.Charts.Basic, notesArray, j, reader.GetString()!);
                else if (i == 1)
                    WriteSingleChart(songInfo.Charts.Advanced, notesArray, j, reader.GetString()!);
                else if (i == 2)
                    WriteSingleChart(songInfo.Charts.Expert, notesArray, j, reader.GetString()!);
                else if (i == 3)
                    WriteSingleChart(songInfo.Charts.Master, notesArray, j, reader.GetString()!);
                else if (i == 4)
                {
                    songInfo.Charts.ReMaster = new();
                    WriteSingleChart(songInfo.Charts.ReMaster, notesArray, j, reader.GetString()!);
                }
                reader.Read();
                i++;

                //EndObject
                reader.Read();

                ArrayPool<int>.Shared.Return(notesArray);

            }
            //endArray
            reader.Read();

            songInfo.Charts.Basic.Id = cidArray[0];
            songInfo.Charts.Basic.DS = dsArray[0];
            songInfo.Charts.Basic.Level = levelArray[0];

            songInfo.Charts.Advanced.Id = cidArray[1];
            songInfo.Charts.Advanced.DS = dsArray[1];
            songInfo.Charts.Advanced.Level = levelArray[1];

            songInfo.Charts.Expert.Id = cidArray[2];
            songInfo.Charts.Expert.DS = dsArray[2];
            songInfo.Charts.Expert.Level = levelArray[2];

            songInfo.Charts.Master.Id = cidArray[3];
            songInfo.Charts.Master.DS = dsArray[3];
            songInfo.Charts.Master.Level = levelArray[3];

            if (i > 4)
            {
                songInfo.Charts.ReMaster!.Id = cidArray[4];
                songInfo.Charts.ReMaster.DS = dsArray[4];
                songInfo.Charts.ReMaster.Level = levelArray[4];
            }

            ArrayPool<int>.Shared.Return(cidArray);
            ArrayPool<double>.Shared.Return(dsArray);
            ArrayPool<string>.Shared.Return(levelArray);


            //StartObject
            reader.Read();

            //basic_info.title
            reader.Read();
            reader.Read();

            //PropertyName artist
            reader.Read();
            reader.Read();
            songInfo.Artist = reader.GetString()!;

            // PropertyName genre
            // String
            reader.Read();
            reader.Read();
            songInfo.Genre = reader.GetString()!;

            //PropertyName bpm
            reader.Read();
            reader.Read();
            songInfo.Bpm = reader.GetInt32();

            // PropertyName release_date
            //String
            reader.Read();
            reader.Read();

            if (DateTime.TryParse(reader.GetString(), out DateTime dateTime))
                songInfo.ReleaseDate = dateTime;

            //PropertyName from
            //String
            reader.Read();
            reader.Read();

            songInfo.From = reader.GetString()!;

            //PropertyName is_new
            //False
            reader.Read();
            reader.Read();

            songInfo.IsNew = reader.GetBoolean();
            //endobj
            reader.Read();
            //endobj
            reader.Read();
            //end obj
            reader.Read();

            dict.Add(songInfo.Id, songInfo);
        }
        // endArray
        reader.Read();
        Debug.Assert(reader.CurrentDepth == 0);
        return dict;
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<int, SongInfo> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}