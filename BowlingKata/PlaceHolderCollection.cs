using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public class PlaceHolderCollection
    {
        private readonly List<PlaceHolder> _placeHolders = new List<PlaceHolder>();

        public void AddPlaceHolder(int frameNumber)
        {
            _placeHolders.Add(new PlaceHolder(frameNumber));
        }

        public int FindFirstEmptyPlaceHolder()
        {
            return _placeHolders.FindIndex(PlaceHolderExtensions.IsEmpty);
        }

        public int CountOfEmptyPlaceHoldersForFrameNumber(int frameNumber)
        {
            return _placeHolders.Count(ph => ph.IsEmpty() && ph.FrameNumber == frameNumber);
        }

        public int CountOfPlaceHoldersForFrameNumber(int frameNumber)
        {
            return _placeHolders.Count(ph => ph.FrameNumber == frameNumber);
        }

        public void UpdatePlaceHolder(int index, int roll)
        {
            _placeHolders[index] = _placeHolders[index].With(roll);
        }

        public PlaceHolder this[int index]
        {
            get { return _placeHolders[index]; }
        }
    }
}
