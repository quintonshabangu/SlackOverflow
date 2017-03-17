using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Controllers.Helpers
{
    public class RefereeHelper
    {
        private static RefereeHelper _Instance;

        public static RefereeHelper Instance
        {
            get
            {
                if (_Instance == null)
                _Instance = new RefereeHelper();

                return _Instance;
            }
        }

        private RefereeHelper() {}

        public Points QuestionerPostsQuestionPoints()
        {
            return getPoints(PointChangeType.QuestionerPostsQuestion);
        }

        public Points AnswererPostsAnswerPoints()
        {
            return getPoints(PointChangeType.AnswererPostsAnswer);
        }

        public Points VoterVotedQuestionUpPoints()
        {
            return getPoints(PointChangeType.VoterVotedQuestionUp);
        }

        public Points VoterVotedQuestionDown()
        {
            return getPoints(PointChangeType.VoterVotedQuestionDown);
        }

        public Points VoterVotedAnswerUp()
        {
            return getPoints(PointChangeType.VoterVotedAnswerUp);
        }

        public Points VoterVotedAnswerDown()
        {
            return getPoints(PointChangeType.VoterVotedAnswerDown);
        }

        private Points getPoints(PointChangeType Type)
        {
            switch (Type)
            {
                case PointChangeType.QuestionerPostsQuestion:
                    return new Points(3, 0);

                case PointChangeType.AnswererPostsAnswer:
                    return new Points(5, 0);

                case PointChangeType.VoterVotedQuestionUp:
                    return new Points(1, 1);

                case PointChangeType.VoterVotedQuestionDown:
                    return new Points(-1, 0);

                case PointChangeType.VoterVotedAnswerUp:
                    return new Points(1, 1);

                case PointChangeType.VoterVotedAnswerDown:
                    return new Points(-1, 1);

                default:
                    return new Points(0, 0);
            }
        }
    }

    public class Points
    {
        public int PosterPoints { get; }

        public int VoterPoints { get; }

        public Points(int PosterPoints, int VoterPoints)
        {
            this.PosterPoints = PosterPoints;
            this.VoterPoints = VoterPoints;
        }
    }

    public enum PointChangeType
    {
        //Affects questioner
        QuestionerPostsQuestion,

        //Affects answerer
        AnswererPostsAnswer,

        // Affects questioner and Voter
        VoterVotedQuestionUp,
        VoterVotedQuestionDown,

        // Affects answerer and Voter
        VoterVotedAnswerUp,
        VoterVotedAnswerDown,
    }
}