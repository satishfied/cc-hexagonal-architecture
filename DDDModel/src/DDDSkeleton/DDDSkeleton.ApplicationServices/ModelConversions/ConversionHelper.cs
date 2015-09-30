using System.Collections.Generic;
using System.Linq;
using DDDSkeleton.ApplicationServices.ViewModels;
using DDDSkeleton.Domain;

namespace DDDSkeleton.ApplicationServices.ModelConversions
{
    public static class ConversionHelper
    {
        public static ScreeningViewModel ConvertToViewModel(this Screening screening)
        {
            var viewModel = new ScreeningViewModel
            {
                Id = screening.Id,
                Candidate = screening.Candidate,
                Recruiter = screening.Recruiter,
                Date = screening.Date,
                GlobalEvaluation = screening.GlobalEvaluation,
                Location = screening.Location,
                Remark = screening.Remark,
                ExcerciceViewModels = screening.Excercises.ConvertToViewModels().ToList(),
                KnowledgeDomainViewModels = screening.KnowledgeDomains.ConvertToViewModels().ToList(),
            };

            return viewModel;
        }

        public static IEnumerable<ScreeningViewModel> ConvertToViewModels(this IEnumerable<Screening> screenings)
        {
            return screenings.Select(screening => screening.ConvertToViewModel()).ToList();
        }

        public static ExcerciceViewModel ConvertToViewModel(this Excercise excercise)
        {
            var viewModel = new ExcerciceViewModel
            {
                Name = excercise.Name,
                Number = excercise.Number,
                EvaluationViewModels = excercise.Evaluations.ConvertToViewModels().ToList()
            };

            return viewModel;
        }

        public static IEnumerable<ExcerciceViewModel> ConvertToViewModels(this IEnumerable<Excercise> excercises)
        {
            return excercises.Select(excercise => excercise.ConvertToViewModel()).ToList();
        }

        public static KnowledgeDomainViewModel ConvertToViewModel(this KnowledgeDomain domain)
        {
            var viewModel = new KnowledgeDomainViewModel
            {
                Name = domain.Name,
                Number = domain.Number,
                EvaluationViewModels = domain.Evaluations.ConvertToViewModels().ToList()
            };

            return viewModel;
        }

        public static IEnumerable<KnowledgeDomainViewModel> ConvertToViewModels(
            this IEnumerable<KnowledgeDomain> domains)
        {
            return domains.Select(excercise => excercise.ConvertToViewModel()).ToList();
        }

        public static EvaluationViewModel ConvertToViewModel(this Evaluation evaluation)
        {
            var viewModel = new EvaluationViewModel
            {
                Remark = evaluation.Remark,
                Score = (int) evaluation.Score
            };

            return viewModel;
        }

        public static IEnumerable<EvaluationViewModel> ConvertToViewModels(this IEnumerable<Evaluation> evaluations)
        {
            return evaluations.Select(evaluation => evaluation.ConvertToViewModel()).ToList();
        }
    }
}